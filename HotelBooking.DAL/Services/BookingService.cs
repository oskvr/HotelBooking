using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Wrappers;
using HotelBooking.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public async Task Create(Booking booking)
		{
			throw new NotImplementedException();
		}

		private async Task<Room> GetFirstAvailableRoomAsync(BookingWrapper booking)
		{
			var matchingRooms = await dbContext.Rooms.Include(room=>room.Bookings).Where(room => room.HotelId == booking.HotelId
				&& room.RoomTypeId == booking.RoomType.Id).ToListAsync();
			Room roomToBeBooked = null;
			foreach (Room room in matchingRooms)
			{
				bool isAvailable = room.IsAvailableBetweenDates(booking.CheckInDate, booking.CheckOutDate);
				if(isAvailable)
				{
					roomToBeBooked = room;
					break;
				}
				
			}
			return roomToBeBooked;
		}

		public async Task Create(BookingWrapper booking)
		{
			Room room = await GetFirstAvailableRoomAsync(booking);
			Debug.WriteLine(room);
			if (room is null)
			{
				throw new Exception("No rooms available for selected period");
			}
			Booking newBooking = new()
			{
				CheckInDate = booking.CheckInDate,
				CheckOutDate = booking.CheckOutDate,
				HotelId = booking.HotelId,
				UserId = store.CurrentUser.Id,
				RoomId = room.Id,
			};
			await dbContext.Bookings.AddAsync(newBooking);
			await dbContext.SaveChangesAsync();

			//// Adding to the many-to-many join table
			foreach (var extra in booking.SelectedBookingExtras)
			{
				//newBooking.BookingExtras.Add(new BookingExtra
				//{
				//	Id = extra.Id
				//});
				newBooking.BookingExtras.Add(extra);
			}
			await dbContext.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var bookingToDelete = await dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);
			if(bookingToDelete is not null)
			{
				dbContext.Remove(bookingToDelete);
				await dbContext.SaveChangesAsync();
			}
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
			return await dbContext.Bookings.Include(booking => booking.Hotel).Where(booking => booking.UserId == store.CurrentUser.Id).ToListAsync();
		}
	}
}
