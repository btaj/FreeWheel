using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MovieApi.Http
{
	public class NotFoundOnExceptionAttribute : HttpStatusCodeOnExceptionAttribute
	{
		public NotFoundOnExceptionAttribute(params Type[] exceptionTypes):base(HttpStatusCode.NotFound, exceptionTypes)
		{

		}
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (ExceptionTypes.Any(x => x.IsInstanceOfType(actionExecutedContext.Exception)))
				actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound,"Requested Entity Not Found");
		}
	}


}