using System.Collections.Generic;

namespace Comcast.DataBase.Models
{
	public class User
	{
		public User()
		{
			this.UserMovieRating = new List<UserMovieRating>();
		}

		public int UserId { get; set; }
		public string Name { get; set; }

		public virtual ICollection<UserMovieRating> UserMovieRating { get; set; }
	}
}