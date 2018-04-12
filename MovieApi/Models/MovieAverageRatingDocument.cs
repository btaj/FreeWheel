using System;

namespace MovieApi.Models
{
	public class MovieAverageRatingDocument : MovieDocument
	{		
		public string AverageRating { get; set; }
		public double AverageRatingBeforeRounding { get; set; }
		public int Count { get; set; }
		public int Sum { get; set; }

	}

}