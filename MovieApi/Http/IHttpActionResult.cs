using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MovieApi.Http
{

	public interface IHttpActionResult<T> : IHttpActionResult
	{
		HttpResponseMessage Response { get; }

	}
}