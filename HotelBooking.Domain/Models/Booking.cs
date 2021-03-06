using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public class Booking : BaseEntity
	{
		public DateTime CheckInDate { get; set; } = DateTime.Now;
		public DateTime CheckOutDate { get; set; } = DateTime.Now;
		public int UserId { get; set; }
		public User User { get; set; }
		public int RoomId { get; set; }
		public Room Room { get; set; }
		public int HotelId { get; set; }
		public Hotel Hotel { get; set; }
		public ICollection<BookingExtra> BookingExtras { get; set; } = new List<BookingExtra>();

		// Computed Properties

		public int TotalDays => (CheckOutDate - CheckInDate).Days;
		public double TotalPrice => (Room.RoomType.PricePerNight * TotalDays) + BookingExtras.Sum(extra => extra.Cost);
	}
}
