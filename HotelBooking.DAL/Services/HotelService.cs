using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public class HotelService : IHotelService
	{
		private readonly HotelBookingDbContext dbContext;

		public HotelService(HotelBookingDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task Create(Hotel entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
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

		public async Task<IEnumerable<Hotel>> GetAll()
		{
			return await dbContext.Hotels.Include(hotel => hotel.Ratings).Include(hotel=>hotel.Rooms).ToListAsync();
		}
		public async Task<List<Room>> GetAvailableRoomsBetweenDates(int hotelId, DateTime checkInDate, DateTime checkOutDate)
		{
			List<Room> allRooms = await dbContext.Rooms.Include(room => room.Bookings).Where(room => room.HotelId == hotelId).ToListAsync();
			List<Room> availableRooms = new();
			foreach (Room room in allRooms)
			{
				bool isAvailable = room.IsAvailableBetweenDates(checkInDate, checkOutDate);
				if (isAvailable)
				{
					availableRooms.Add(room);
				}

			}
			return availableRooms;
		}

		public async Task<List<RoomType>> GetAvailableRoomTypesBetweenDates(int hotelId, DateTime checkInDate, DateTime checkOutDate)
		{
			var availableRooms = await GetAvailableRoomsBetweenDates(hotelId, checkInDate, checkOutDate);
			return availableRooms.Select(room => room.RoomType).Distinct().ToList();
		}
		//public async Task<IEnumerable<RoomType>> GetAvailableRoomTypesBetweenDates(int hotelId, DateTime checkInDate, DateTime checkOutDate)
		//{
		//	Hotel hotel = await dbContext.Hotels.Include(hotel=>hotel.Rooms).FirstOrDefaultAsync(hotel => hotel.Id == hotelId);
		//	var availableRoomTypes = hotel.Rooms.Where(room=>room.isa)
		//}

	}
}
