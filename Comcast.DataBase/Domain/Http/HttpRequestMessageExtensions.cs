using Comcast.DataBase.Models;
using MovieApi.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MovieApi.Http
{
	public static class HttpRequestMessageExtensions
	{
		public static bool IsIn(this string genre, List<string> genres)
		{
			return genres.Contains(genre);
		}
		public static ErrorHttpActionResult<T> CreateErrorHttpActionResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode)
		{
			return new ErrorHttpActionResult<T>(request, statusCode);
		}
		public static ErrorHttpActionResult<T>  CreateErrorHttpActionResponseWithFilter<T>(this HttpRequestMessage request, HttpStatusCode statusCode)
		{
			return new ErrorHttpActionResult<T>(request, statusCode);
		}
		public static async Task<TypedHttpActionResult<T>> CreateTypedResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T result)
		{
			return new TypedHttpActionResult<T>(request, statusCode, result);
		}

		public static async Task<TypedHttpActionResult<IEnumerable<T>>> CreateTypedResponseWithFilter<T>(this HttpRequestMessage request, HttpStatusCode statusCode, Func<IEnumerable<KeyValuePair<string, string>>, Task<IEnumerable<T>>> getResults)
		{
			var items = request.GetQueryNameValuePairs();
			var results = await getResults(items);
			
			return new TypedHttpActionResult<IEnumerable<T>>(request, statusCode, results);

		}

		

	}


}