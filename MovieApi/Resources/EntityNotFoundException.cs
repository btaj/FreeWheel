using System;

namespace MovieApi.Resources
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException() { }
		public EntityNotFoundException(string entity) : base("No Movie Found")
		{ }
	}
}