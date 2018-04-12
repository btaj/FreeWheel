using Comcast.DataBase.Models;
using System.Data.Entity;

namespace Comcast.DataBase.Context
{
	public class MainDbContext : DbContext, IMainDbContext
	{

		public MainDbContext() : base("name=movierating")
		{
			//if(env == "dev")
				Database.SetInitializer(new DbInitialiser());
		}

		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}

		public DbSet<Movie> Movies { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserMovieRating> UserMovieRatings { get; set; }



	}

}
