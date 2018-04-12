using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieApi.Resources;
using Moq;
using Comcast.DataBase.Context;
using Comcast.DataBase.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MovieApi.Tests
{
	[TestClass]
	public class MovieResourceTester
	{
		private List<Movie> moviesList;
		private List<User> userList;
		private List<UserMovieRating> userRatingList;
		private Mock<IMainDbContext> db;


		[TestInitialize]
		public void Setup()
		{
			 moviesList = new List<Movie> {
				new Movie { MovieId = 1, Title = "A Separation", YearOfRelease = 2011, Genre = "DRAMA", RunningTime = 90 },
				new Movie { MovieId = 2, Title = "Taken", YearOfRelease = 1998, Genre = GenreEnum.ACTION.ToString(), RunningTime = 120 },
				new Movie { MovieId = 3, Title = "Mr Bean", YearOfRelease = 2002, Genre = GenreEnum.COMEDY.ToString(), RunningTime = 120 },
				new Movie { MovieId = 4, Title = "Lost", YearOfRelease = 2009, Genre = GenreEnum.ADVENTURE.ToString(), RunningTime = 120 },
				new Movie { MovieId = 5, Title = "The Box", YearOfRelease = 2018, Genre = GenreEnum.THRILLER.ToString(), RunningTime = 120 },
				new Movie { MovieId = 6, Title = "Up", YearOfRelease = 2009, Genre = GenreEnum.ROMANCE.ToString(), RunningTime = 120 },
			};

			 userList = new List<User> {
				new User { UserId = 1, Name = "Sam" },
				new User { UserId = 2, Name = "Cam" },
				new User { UserId = 3, Name = "Tom" },
				new User { UserId = 4, Name = "Pam" },
			};
			 userRatingList = new List<UserMovieRating> {
				 new UserMovieRating { UserMovieRatingId = 1, MovieId = 1, UserId = 1, Rating = 4, Movie = moviesList.First(x => x.MovieId == 1) },
				 new UserMovieRating { UserMovieRatingId = 2, MovieId = 2, UserId = 1, Rating = 4, Movie = moviesList.First(x => x.MovieId == 2) },
				 new UserMovieRating { UserMovieRatingId = 3, MovieId = 1, UserId = 2, Rating = 4, Movie = moviesList.First(x => x.MovieId == 1) },
				 new UserMovieRating { UserMovieRatingId = 4, MovieId = 2, UserId = 2, Rating = 1, Movie = moviesList.First(x => x.MovieId == 2) },
				 new UserMovieRating { UserMovieRatingId = 5, MovieId = 1, UserId = 3, Rating = 2, Movie = moviesList.First(x => x.MovieId == 1) },
				 new UserMovieRating { UserMovieRatingId = 6, MovieId = 2, UserId = 3, Rating = 5, Movie = moviesList.First(x => x.MovieId == 2) },
				 new UserMovieRating { UserMovieRatingId = 7, MovieId = 1, UserId = 4, Rating = 1, Movie = moviesList.First(x => x.MovieId == 1) },
				 new UserMovieRating { UserMovieRatingId = 8, MovieId = 3, UserId = 1, Rating = 1, Movie = moviesList.First(x => x.MovieId == 3) },
				 new UserMovieRating { UserMovieRatingId = 9, MovieId = 4, UserId = 1, Rating = 5, Movie = moviesList.First(x => x.MovieId == 4) },
				 new UserMovieRating { UserMovieRatingId = 10, MovieId = 5, UserId = 1, Rating = 4, Movie = moviesList.First(x => x.MovieId == 5) },
				 new UserMovieRating { UserMovieRatingId = 11, MovieId = 6, UserId = 1, Rating = 4, Movie = moviesList.First(x => x.MovieId == 5) },
			};
			var movies = Helper.GetQueryableMockDbSet<Movie>(moviesList);
			var userRatings = Helper.GetQueryableMockDbSet<UserMovieRating>(userRatingList);
			var users = Helper.GetQueryableMockDbSet<User>(userList);


			db = new Mock<IMainDbContext>();
			db.Setup(x => x.UserMovieRatings).Returns(userRatings);
			db.Setup(x => x.UserMovieRatings.Include(nameof(Movie))).Returns(userRatings);
			db.Setup(x => x.Movies).Returns(movies);
			db.Setup(x => x.Users).Returns(users);
		}


		[TestMethod]
		public async Task Top5Movies()
		{
			//Given	above
			var underTest = new MovieResource(db.Object);

			//When
			var test = await underTest.GetMovieAverageRating();

			//Then
			Assert.IsTrue(test.Count() == 5);
			Assert.IsTrue(test.First(x => x.MovieId == 1).AverageRating == "3");
			Assert.IsTrue(test.First(x => x.MovieId == 2).AverageRating == "3.5");
		}

		[TestMethod]
		[Ignore]
		public async Task Top5MovieRatingByUser_Success()
		{
			//Given
			var underTest = new MovieResource(db.Object);

			//When
			var test = await underTest.GetUserMovieRating(1);

			//Then
			Assert.IsTrue(test.Count() == 5);
			Assert.IsTrue(test.Count(x => x.Rating == 4) == 4);

		}
		[TestMethod]
		[Ignore]
		[ExpectedException(typeof(EntityNotFoundException))]
		public async Task Top5MovieRatingByUser_Throws_NotFoundException()
		{
			var underTest = new MovieResource(db.Object);
			var test = await underTest.GetUserMovieRating(10);
		}

		[TestMethod]
		[Ignore]
		public async Task Search_With_Only_Title()
		{
			var underTest = new MovieResource(db.Object);
			var query = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Title" , "separation")
			};
			var test = await underTest.Search(query);

			Assert.IsTrue(test.Count() == 1);
		}

		[TestMethod]
		[Ignore]
		public async Task Search_With_Only_YearOfRelease()
		{
			var underTest = new MovieResource(db.Object);
			var query = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("YearOfRelease" , "2009")
			};
			var test = await underTest.Search(query);

			Assert.IsTrue(test.Count() == 2);
		}

		[TestMethod]
		[Ignore]
		public async Task Search_With_Only_Genres()
		{
			var underTest = new MovieResource(db.Object);
			var query = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Genre" , "Drama"),
				new KeyValuePair<string, string>("Genre" , "Action")
			};
			var test = await underTest.Search(query);

			Assert.IsTrue(test.Count() == 2);
		}

		[TestMethod]
		[Ignore]
		public async Task Search_With_Genres_and_Title()
		{
			var underTest = new MovieResource(db.Object);
			var query = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Genre" , "Drama"),
				new KeyValuePair<string, string>("Genre" , "Action"),
				new KeyValuePair<string, string>("Title" , "t")

			};
			var test = await underTest.Search(query);

			Assert.IsTrue(test.Count() == 4);
		}
	}
}
