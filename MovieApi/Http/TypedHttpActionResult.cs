using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApi.Http
{
	public class TypedHttpActionResult<T> : IHttpActionResult<T>
	{
		public HttpResponseMessage Response { get; private set; }


		public TypedHttpActionResult(HttpRequestMessage request, HttpStatusCode statusCode, T result)
		{
			Response = request.CreateResponse(statusCode, result, new MediaTypeHeaderValue("application/json"));
		}



		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(Response);
		}
	}

	public class ErrorHttpActionResult<T> : IHttpActionResult<T>
	{
		public HttpResponseMessage Response { get; private set; }
		public ErrorHttpActionResult(HttpRequestMessage request, HttpStatusCode code)
		{
			Response = request.CreateErrorResponse(code, "There was an error with the requested resource");
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(Response);
		}
	}
	
}