using Comcast.DataBase.Models;
using System.Data.Entity;

namespace Comcast.DataBase.Context
{
	public class DbInitialiser : DropCreateDatabaseIfModelChanges<MainDbContext>
	{
		public DbInitialiser()
		{
		}
		protected override void Seed(MainDbContext context)
		{
			context.Movies.Add(new Movie { MovieId = 1, Title = "A Separation", YearOfRelease = 2011, Genre = GenreEnum.DRAMA.ToString(), RunningTime = 90 });
			context.Movies.Add(new Movie { MovieId = 2, Title = "Taken", YearOfRelease = 2008, Genre = GenreEnum.ACTION.ToString(), RunningTime = 120 });
			context.Movies.Add(new Movie { MovieId = 3, Title = "The Room", YearOfRelease = 2012, Genre = GenreEnum.THRILLER.ToString(), RunningTime=120 });
			context.Movies.Add(new Movie { MovieId = 4, Title = "The Guest", YearOfRelease = 2014, Genre = GenreEnum.HORROR.ToString(), RunningTime = 95 });
			context.Movies.Add(new Movie { MovieId = 5, Title = "Up", YearOfRelease = 2009, Genre = GenreEnum.ANIMATION.ToString(), RunningTime = 110 });
			context.Movies.Add(new Movie { MovieId = 6, Title = "Titanic", YearOfRelease = 1997, Genre = GenreEnum.ROMANCE.ToString(), RunningTime = 95 });
			context.Movies.Add(new Movie { MovieId = 7, Title = "Mr Bean", YearOfRelease = 2010, Genre = GenreEnum.COMEDY.ToString(), RunningTime = 95 });
			context.Movies.Add(new Movie { MovieId = 8, Title = "Sleeping Beauty", YearOfRelease = 1990, Genre = GenreEnum.ANIMATION.ToString(), RunningTime = 95 });

			context.Users.Add(new User { UserId = 1, Name = "Sam" });
			context.Users.Add(new User { UserId = 2, Name = "Cam" });
			context.Users.Add(new User { UserId = 3, Name = "Ali" });
			context.Users.Add(new User { UserId = 4, Name = "Elli" });
			context.Users.Add(new User { UserId = 5, Name = "Bee" });
			context.Users.Add(new User { UserId = 6, Name = "See" });
			context.Users.Add(new User { UserId = 7, Name = "Hady" });
			context.Users.Add(new User { UserId = 8, Name = "Barie" });
			context.Users.Add(new User { UserId = 9, Name = "Charly" });
			context.Users.Add(new User { UserId = 10, Name = "Holly" });




			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 1, MovieId = 1, UserId = 1, Rating = 5 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 2, MovieId = 2, UserId = 1, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 3, MovieId = 3, UserId = 1, Rating = 3 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 4, MovieId = 4, UserId = 1, Rating = 2 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 5, MovieId = 5, UserId = 1, Rating = 3 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 6, MovieId = 6, UserId = 1, Rating = 2 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 7, MovieId = 4, UserId = 2, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 8, MovieId = 5, UserId = 2, Rating = 4 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 9, MovieId = 1, UserId = 3, Rating = 5 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 10, MovieId = 6, UserId = 3, Rating = 2 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 11, MovieId = 3, UserId = 3, Rating = 4 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 12, MovieId = 2, UserId = 4, Rating = 5 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 18, MovieId = 6, UserId = 4, Rating = 5 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 13, MovieId = 5, UserId = 5, Rating = 5 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 14, MovieId = 6, UserId = 5, Rating = 5 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 15, MovieId = 6, UserId = 6, Rating = 1 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 16, MovieId = 2, UserId = 6, Rating = 1 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 17, MovieId = 4, UserId = 6, Rating = 5 });
			

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 19, MovieId = 4, UserId = 7, Rating = 4 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 20, MovieId = 3, UserId = 8, Rating = 3 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 21, MovieId = 2, UserId = 8, Rating = 5 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 22, MovieId = 1, UserId = 9, Rating = 5 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 23, MovieId = 2, UserId = 9, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 24, MovieId = 3, UserId = 9, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 25, MovieId = 4, UserId = 9, Rating = 5 });

			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 26, MovieId = 2, UserId = 10, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 27, MovieId = 1, UserId = 10, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 28, MovieId = 4, UserId = 10, Rating = 3 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 29, MovieId = 5, UserId = 10, Rating = 4 });
			context.UserMovieRatings.Add(new UserMovieRating { UserMovieRatingId = 30, MovieId = 6, UserId = 10, Rating = 4 });

			context.SaveChanges();
			base.Seed(context);

		}

	}

}
