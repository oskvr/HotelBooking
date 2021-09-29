using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
    public class BookingsListViewModel:BindableBase, INavigationAware
    {
		public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();

		private readonly IBookingService bookingService;

		public BookingsListViewModel(IBookingService bookingService)
		{
			this.bookingService = bookingService;
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
			async void LoadBookings()
			{
				var bookings = await bookingService.GetAll();
				Bookings.Clear();
				foreach (var booking in bookings)
				{
					Bookings.Add(booking);
				}
			}
			LoadBookings();
		}
	}
}
