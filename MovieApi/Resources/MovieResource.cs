using Comcast.DataBase.Context;
using Comcast.DataBase.Models;
using MovieApi.Http;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Linq.SqlClient;

namespace MovieApi.Resources
{
	/// <summary>
	/// A class to  manage movie resources 
	/// </summary>
	public class MovieResource : IMovieResource
	{
		private readonly IMainDbContext dbContext;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dbContext"></param>
		public MovieResource(IMainDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		///  return the details of the top 5 movies based on total user average ratings;
		///  In case of a rating draw, (e.g. 2 movies have 3.768 average rating) return them by ascending title alphabetical order
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>Returns list of movies details</returns>
		public async Task<IEnumerable<MovieUserRatingDocument>> GetUserMovieRating(int userId)
		{		
			var query = from c in dbContext.Movies.AsQueryable()
						join d in dbContext.UserMovieRatings.AsQueryable()
						on c.MovieId equals d.MovieId
						where d.UserId == userId
						select new MovieUserRatingDocument
						{
							MovieId = c.MovieId,
							Genre = c.Genre.ToString(),
							Title = c.Title,
							YearOfRelease = c.YearOfRelease.ToString(),
							RunningTime = c.RunningTime.ToString(),
							Rating = d.Rating
						};
			var result = query.OrderByDescending(x => x.Rating).ThenBy(x => x.Title).Take(5).ToList();

			if (!result.Any())
				throw new EntityNotFoundException();

			return await Task.FromResult(result);

		}

		/// <summary>
		///  return the details of the top 5 movies based on total user average ratings;
		///  In case of a rating draw, (e.g. 2 movies have 3.768 average rating) return them by ascending title alphabetical order
		/// </summary>
		/// <returns>List of movies details including the movie Average rating</returns>
		public async Task<IEnumerable<MovieAverageRatingDocument>> GetMovieAverageRating()
		{
			var ratings = dbContext.UserMovieRatings.Include(nameof(Movie));
			var query = ratings
				.GroupBy(l => l.MovieId)
				.Select(cl => new MovieAverageRatingDocument
				{
					MovieId = cl.FirstOrDefault().MovieId,
					Genre = cl.FirstOrDefault().Movie.Genre.ToString(),
					RunningTime = cl.FirstOrDefault().Movie.RunningTime.ToString(),
					Title = cl.FirstOrDefault().Movie.Title,
					YearOfRelease = cl.FirstOrDefault().Movie.YearOfRelease.ToString(),
					AverageRatingBeforeRounding = cl.Average(x => x.Rating),
					Sum = cl.Sum(c => c.Rating),
					Count = cl.Count()
				});
					
			var result = query.OrderByDescending(x => x.AverageRatingBeforeRounding).ThenBy(x => x.Title).Take(5).ToList().Select(x => 
			{
				x.AverageRating = (Math.Round(2 * Convert.ToDecimal(x.AverageRatingBeforeRounding), MidpointRounding.AwayFromZero) / 2).ToString("G");
				return x;
			}).ToList();
			
			return await Task.FromResult(result);
		}


		/// <summary>
		///  return the details of movies that pass	certain filter criteria provided by the api consumers.
		///  At least one filter criteria should be provided by the caller, else the api should return an error;
		///  The criteria filters can be one or more of the following:
		///  Title or partial title of the movie;
		///  Year of release;
		///  Genre(s).
		/// </summary>
		/// <param name="queryString">{"Title","abc"}</param>
		/// <returns>Details of the top 5 movies Rated by users satisfying the query criteria</returns>
		public async Task<IEnumerable<MovieAverageRatingDocument>> Search(IEnumerable<KeyValuePair<string, string>> queryString)
		{
			var filteredMovies = BuildQuery(queryString);
			var ratings = dbContext.UserMovieRatings.AsQueryable();
			var qry = (from p in filteredMovies
					 join c in ratings on p.MovieId equals c.MovieId into j1
					 from j2 in j1.DefaultIfEmpty(new UserMovieRating())
					 group j2 by p.MovieId into grouped
					 let m = filteredMovies.First(x => x.MovieId == grouped.Key)
					 select new MovieAverageRatingDocument
					 {
						 MovieId = grouped.Key,
						 Count = grouped.Count(t => t.Movie != null),
						 Genre = m.Genre,
						 Title = m.Title,
						 YearOfRelease = m.YearOfRelease.ToString(),
						 RunningTime = m.RunningTime.ToString(),
						 AverageRatingBeforeRounding = grouped.Average(x => x.Rating),
						 Sum = grouped.Sum(x => x.Rating)
					 });
			
			

			var result = qry.OrderByDescending(x => x.AverageRatingBeforeRounding).ThenBy(x => x.Title).Take(5).ToList().Select(x =>
			{
				x.AverageRating =(Math.Round(2 * Convert.ToDecimal(x.AverageRatingBeforeRounding), MidpointRounding.AwayFromZero) / 2).ToString("F");
				return x;
			}).ToList();

			if (!result.Any())
				throw new EntityNotFoundException();

			return await Task.FromResult(result);

		}

		public async Task<bool> PostUserRating(int userId, MovieRatingRequest request)
		{
			var user = dbContext.Users.SingleOrDefault(x => x.UserId == userId);
			if (user == null)
				throw new EntityNotFoundException("User Not Found");

			var movie = dbContext.Movies.SingleOrDefault(x => x.MovieId == request.MovieId);
			if (movie == null)
				throw new EntityNotFoundException("Movie Not Found");

			if( request.Rating > 5 || request.Rating < 1)
				throw new BadRequestException("Bad Request");


			var rating = dbContext.UserMovieRatings.FirstOrDefault(x => x.MovieId == movie.MovieId && x.UserId == user.UserId);
			if(rating == null)
			{
				dbContext.UserMovieRatings.Add(new UserMovieRating { MovieId = movie.MovieId, UserId = user.UserId, Rating = request.Rating});
			}
			else
			{
				rating.Rating = request.Rating;				
			}
			dbContext.SaveChanges();
			return await Task.FromResult(true);
		}

		private IEnumerable<Movie> BuildQuery(IEnumerable<KeyValuePair<string, string>> query)
		{
			if (query.All(x => !x.Key.Equals(nameof(Movie.Title),StringComparison.InvariantCultureIgnoreCase) && !x.Key.Equals(nameof(Movie.Genre), StringComparison.InvariantCultureIgnoreCase) && !x.Key.Equals(nameof(Movie.YearOfRelease),StringComparison.InvariantCultureIgnoreCase)))
				throw new BadRequestException();

			string title = null;
			int release = 0;
			var genres = new List<string>();
			if (query.Any(x => x.Key.Equals(nameof(Movie.Title), StringComparison.InvariantCultureIgnoreCase)))
				title = query.First(x => x.Key.Equals(nameof(Movie.Title), StringComparison.InvariantCultureIgnoreCase)).Value;

			if (query.Any(x => x.Key.Equals(nameof(Movie.YearOfRelease), StringComparison.InvariantCultureIgnoreCase)))
				release = Convert.ToInt32(query.First(x => x.Key.Equals(nameof(Movie.YearOfRelease), StringComparison.InvariantCultureIgnoreCase)).Value);

			if (query.Count(x => x.Key == nameof(Movie.Genre)) > 0)
			{
				genres.AddRange(query.Where(x => x.Key == nameof(Movie.Genre)).Select(y => y.Value.ToLower()));
			}

			var movies = dbContext.Movies.AsQueryable();
			if (!string.IsNullOrEmpty(title))
			{
				movies = movies.Where(x => x.Title.ToLower().Contains(title.ToLower()));
			}

			if (release > 0)
			{
				movies = movies.Where(x => x.YearOfRelease == release);
			}
			if (genres.Any())
			{
				movies = movies.Where(x => genres.Contains(x.Genre.ToLower()));
			}
					
			return movies;
		}



		public async Task<IEnumerable<Movie>> GetAll()
		{
			var m = dbContext.Movies.Select(x => x).ToList();
			return await Task.FromResult(m);
		}
	}
}