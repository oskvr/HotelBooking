using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{

	public class RoomType : BaseEntity
	{
		public string Type { get; set; } // eg. Single Room
		public int MaxCapacity { get; set; }
		public int PricePerNight { get; set; }
		public ICollection<Room> Rooms { get; set; } = new List<Room>();
	}
}
