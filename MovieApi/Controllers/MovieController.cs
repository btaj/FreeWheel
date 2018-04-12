using Comcast.DataBase.Context;
using Comcast.DataBase.Models;
using MovieApi.Http;
using MovieApi.Models;
using MovieApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.Results;

namespace MovieApi.Controllers
{
	/// <summary>
	/// Movie Api to handle requests to movie resources
	/// </summary>
	[NotFoundOnException(typeof(EntityNotFoundException))]
	[BadRequestOnException(typeof(BadRequestException))]
	public class MovieController : ApiController
    {
		private readonly IMovieResource movieResource;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="movieResource"></param>
		public MovieController(IMovieResource movieResource)
		{
			this.movieResource = movieResource;
		}


		/// <summary>
		/// return the details of the top 5 movies based on total user average ratings;
		///  In case of a rating draw, (e.g. 2 movies have 3.768 average rating) return them by ascending title alphabetical order
		/// </summary>
		/// <returns></returns>
		[Route("movie/toprating")]
		public async Task<IHttpActionResult<Task<IEnumerable<MovieAverageRatingDocument>>>> GetTopMovies()
		{
			return await Request.CreateTypedResponse(HttpStatusCode.OK, movieResource.GetMovieAverageRating());
		}

		/// <summary>
		///  return the details of the top 5 movies based on total user average ratings;
		///  In case of a rating draw, (e.g. 2 movies have 3.768 average rating) return them by ascending title alphabetical order
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>list of movies</returns>
		[Route("user/{userId}/movierating")]
		public async Task<IHttpActionResult<IEnumerable<MovieUserRatingDocument>>> GetUserMovieRating(int userId)
		{
			return await Request.CreateTypedResponse(HttpStatusCode.OK, await movieResource.GetUserMovieRating(userId));
		}


		/// <summary>
		/// return the details of movies that pass	certain filter criteria provided by the api consumers.
		///  At least one filter criteria should be provided by the caller, else the api should return an error;
		///  The criteria filters can be one or more of the following:
		///  Title or partial title of the movie;
		///  Year of release;
		///  Genre(s).
		/// </summary>
		/// <returns>List of movies found in JSON</returns>
		[HttpGet]
		[Route("search")]
		public async Task<IHttpActionResult<IEnumerable<MovieAverageRatingDocument>>> Search()
		{
			return await Request.CreateTypedResponseWithFilter(HttpStatusCode.OK,  (query) => movieResource.Search(query));	
		}		

		[Route("all")]
		public async Task<IHttpActionResult<Task<IEnumerable<Movie>>>> GetAll()
		{
			return await Request.CreateTypedResponse(HttpStatusCode.OK, movieResource.GetAll());

		}

		[HttpPost]
		[Route("user/{userId}/movierating")]
		public async Task<IHttpActionResult<bool>> Post(int userId, MovieRatingRequest movieRating)
		{
			return await Request.CreateTypedResponse(HttpStatusCode.OK, await movieResource.PostUserRating(userId, movieRating)); 
		}


	}
}
