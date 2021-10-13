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
		public Uri Image { get; set; }
		public ICollection<Room> Rooms { get; set; } = new List<Room>();
		public int Rating { get; set; } = 3;

	}
}
