using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MovieApi.Http
{
	public class BadRequestOnExceptionAttribute : HttpStatusCodeOnExceptionAttribute
	{
		public BadRequestOnExceptionAttribute(params Type[] exceptionTypes) : base(HttpStatusCode.BadRequest, exceptionTypes)
		{

		}
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (ExceptionTypes.Any(x => x.IsInstanceOfType(actionExecutedContext.Exception)))
				actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
		}
	}


}