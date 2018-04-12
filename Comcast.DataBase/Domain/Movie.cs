using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comcast.DataBase.Models
{
	public class Movie
	{

		[Key]
		public int MovieId { get; set; }
		public string Title { get; set; }
		[Range(1900, 3000)]
		public int YearOfRelease { get; set; }
		public string Genre { get; set; }
		public int RunningTime { get; set; }

		

	}
}