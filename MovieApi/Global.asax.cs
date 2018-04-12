using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data.Entity;
using Comcast.DataBase.Context;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using MovieApi.Resources;
using Autofac.Integration.WebApi;

namespace MovieApi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			
			var container = InitialiseContainer();
			var dbContext = (MainDbContext)container.Resolve<IMainDbContext>();

			dbContext.Database.Initialize(true);
			dbContext.Database.CreateIfNotExists();

		}

		private IContainer InitialiseContainer()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var builder = new ContainerBuilder();
			var config = GlobalConfiguration.Configuration;
			//typeof(Global).Assembly
			builder.RegisterControllers(assembly);
			builder.RegisterApiControllers(assembly);


			builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Resource")).AsImplementedInterfaces().InstancePerRequest();

			//builder.RegisterType<MovieResource>().AsImplementedInterfaces().InstancePerRequest();
			
			builder.RegisterType<MainDbContext>().As<IMainDbContext>().SingleInstance();

			var container = builder.Build();
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			return container;
		}
	}
}