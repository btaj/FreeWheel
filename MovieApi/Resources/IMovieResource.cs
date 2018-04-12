using Comcast.DataBase.Models;
using MovieApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Resources
{
	public interface IMovieResource : IResource
	{
		//Task<IEnumerable<Movie>> GetAll();
		Task<IEnumerable<MovieAverageRatingDocument>> GetMovieAverageRating();
		Task<IEnumerable<MovieUserRatingDocument>> GetUserMovieRating(int userId);
		Task<IEnumerable<Movie>> GetAll();
		Task<IEnumerable<MovieAverageRatingDocument>> Search(IEnumerable<KeyValuePair<string, string>> query);
		Task<bool> PostUserRating(int userId, MovieRatingRequest movieRating);
	}
}