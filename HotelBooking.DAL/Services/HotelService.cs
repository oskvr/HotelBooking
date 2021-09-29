using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public class HotelService : IHotelService
	{
		public Task Add(Hotel entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Hotel entity)
		{
			throw new NotImplementedException();
		}

		public Task Edit(Hotel entity)
		{
			throw new NotImplementedException();
		}

		public Task<Hotel> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Hotel>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Room>> GetAvailableRoomsBetweenDates(int hotelId, DateTime checkInDate, DateTime checkOutDate)
		{
			throw new NotImplementedException();
		}
	}
}
