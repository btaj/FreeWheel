using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MovieApi.Http
{
	public class HttpStatusCodeOnExceptionAttribute : ExceptionFilterAttribute
	{
		private readonly HttpStatusCode statusCode;
		protected readonly Type[] ExceptionTypes;
		public HttpStatusCodeOnExceptionAttribute(HttpStatusCode code , params Type[] exceptionTypes)
		{
			this.statusCode = code;
			this.ExceptionTypes = exceptionTypes;
		}

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (ExceptionTypes.Any(x => x.IsInstanceOfType(actionExecutedContext.Exception)))
				actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(statusCode, actionExecutedContext.Exception.Message);
		}
	}


}