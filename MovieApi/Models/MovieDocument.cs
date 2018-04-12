using Comcast.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovieApi.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class MovieDocument
	{
		public int MovieId { get; set; }

		public string Title { get; set; }
		public string YearOfRelease { get; set; }
		public string Genre { get; set; }
		public string RunningTime { get; set; }
	}
}