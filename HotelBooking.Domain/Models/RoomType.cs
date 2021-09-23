using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
    public class RoomType
    {
		public string Title { get; set; }
		public ICollection<Room> Rooms { get; set; }
	}
}
