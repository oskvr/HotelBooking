using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public interface IBookingService : IBaseService<Booking>
	{
		Task Create(BookingWrapper bookingWrapper);
	}
}
