using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
	public class BookingCreateViewModel : BindableBase, INavigationAware
	{
		public Hotel SelectedHotel { get; set; }
		public Booking Booking { get; set; }

		public DelegateCommand	CreateBookingCommand { get; set; }

		private readonly IBookingService bookingService;

		public BookingCreateViewModel(IBookingService bookingService)
		{
			CreateBookingCommand = new DelegateCommand(OnCreateBooking);
			this.bookingService = bookingService;
		}

		private async void OnCreateBooking()
		{
			await bookingService.Add(Booking);
		}
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			var hotelId = navigationContext.Parameters.GetValue<int>("id");
			Booking = new Booking();
			Booking.HotelId = hotelId;
		}
	}
}
