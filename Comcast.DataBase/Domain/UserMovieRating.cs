using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Comcast.DataBase.Models
{

	public class UserMovieRating
	{
		public int UserMovieRatingId { get; set; }
		[Range(1,5)]
		public int Rating { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual User User { get; set; }
		[ForeignKey("Movie")]
		public int MovieId { get; set; }
		public virtual Movie Movie { get; set; }		

	}
}