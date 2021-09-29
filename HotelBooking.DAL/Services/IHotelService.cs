using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public interface IHotelService : IBaseService<Hotel>
	{
		Task<IEnumerable<Room>> GetAvailableRoomsBetweenDates(int hotelId, DateTime checkInDate, DateTime checkOutDate);
		//Task<IEnumerable<Room>> GetAvailableRoomsBetweenDates(Hotel hotel, DateTime checkInDate, DateTime checkOutDate, RoomTypeIds roomTypeId);
	}
}
