using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public class Hotel : BaseEntity
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public Uri ThumbnailImage { get; set; }

		public ICollection<Room> Rooms { get; set; } = new List<Room>();
		public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
		// Computed properties
		public double Rating => Ratings.Sum(rating => rating.Score) / Ratings.Count;
	}
}
