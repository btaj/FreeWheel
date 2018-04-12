using System;

namespace MovieApi.Resources
{
	public class BadRequestException : Exception
	{
		public BadRequestException() { }
		public BadRequestException(string entity) : base("Bad Request")
		{ }
	}
}