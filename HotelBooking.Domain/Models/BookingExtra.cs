using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public class BookingExtra : BaseEntity
	{
		public string Type { get; set; }
		public double Cost { get; set; }
		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
	}
}
