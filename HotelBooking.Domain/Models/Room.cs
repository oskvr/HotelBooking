using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public enum RoomTypeIds
	{
		Single = 1,
		Double = 2,
		Triple = 3,
	}

	public class Room : BaseEntity
	{
		public int RoomTypeId { get; set; }
		public RoomType RoomType { get; set; }
		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
		public int? HotelId { get; set; }
		public Hotel Hotel { get; set; }
		

		// Computed 
		public bool IsAvailableBetweenDates(DateTime checkInDate, DateTime checkOutDate)
		{
			bool DatesOverlap(Booking booking)
			{
				bool endOverlaps = checkOutDate >= booking.CheckInDate && checkOutDate <= booking.CheckOutDate;
				bool startOverlaps = checkInDate >= booking.CheckInDate && checkInDate <= booking.CheckOutDate;
				bool bothOverlap = endOverlaps && startOverlaps;
				return endOverlaps || startOverlaps || bothOverlap;
			}
			return !Bookings.Any(DatesOverlap);
		}

	}
}
