using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public class BookingService : IBookingService
	{
		private readonly HotelBookingDbContext dbContext;
		private readonly HotelService hotelService;
		private readonly GlobalStore store;

		public BookingService(HotelBookingDbContext dbContext, HotelService hotelService, GlobalStore store)
		{
			this.dbContext = dbContext;
			this.hotelService = hotelService;
			this.store = store;
		}
		public async Task Add(Booking booking)
		{
			//var availableRooms = hotelService.GetAvailableRoomsBetweenDates(booking.HotelId, booking.CheckInDate, booking.CheckOutDate);
			booking.UserId = store.CurrentUser.Id;
			booking.RoomId = 1;
			dbContext.Bookings.Add(booking);
			await dbContext.SaveChangesAsync();
			//throw new NotImplementedException();
		}

		public Task Delete(Booking booking)
		{
			throw new NotImplementedException();
		}

		public Task Edit(Booking booking)
		{
			throw new NotImplementedException();
		}

		public async Task<Booking> GetById(int id)
		{
			return await dbContext.Bookings.FirstOrDefaultAsync(booking => booking.Id == id);
		}

		public async Task<IEnumerable<Booking>> GetAll()
		{
			return await dbContext.Bookings.Include(booking => booking.Hotel).Where(booking=>booking.UserId == store.CurrentUser.Id).ToListAsync();
		}
	}
}
