using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models.Wrappers
{
	[AddINotifyPropertyChangedInterface]
	public class HotelWrapper
    {
		public Hotel Hotel{ get; set; }

		public HotelWrapper(Hotel hotel)
		{
			Hotel = hotel;
		}

		private void GetAvailableRoomTypes()
		{
			throw new NotImplementedException();
		}

		public List<RoomType> AvailableRoomTypes { get; set; }
		public bool IsAvailable => AvailableRoomTypes.Count > 0;
			public int FromPrice => AvailableRoomTypes.OrderBy(roomType => roomType.PricePerNight).First().PricePerNight;
	}
}
