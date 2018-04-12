using Comcast.DataBase.Models;
using System.Data.Entity;

namespace Comcast.DataBase.Context
{
	public interface IMainDbContext : IDbContext
	{
		DbSet<Movie> Movies { get; set; }
		DbSet<User> Users { get; set; }
		DbSet<UserMovieRating> UserMovieRatings { get; set; }
	}

}
