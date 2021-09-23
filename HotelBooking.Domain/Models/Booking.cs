using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public class Booking : BaseEntity
	{
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int RoomId { get; set; }
		public Room Room { get; set; }
		public ICollection<BookingExtra> Extras { get; set; }

		// Computed Properties

		public int TotalDays => (CheckOutDate - CheckInDate).Days;
		public double TotalPrice => (Room.PricePerNight * TotalDays) + Extras.Sum(extra => extra.Cost);
	}
}
