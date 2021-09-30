using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
namespace HotelBooking.Presentation.ViewModels
{
	public class BookingsListViewModel : BindableBase, INavigationAware
	{
		public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();
		public bool HasBookings => Bookings is not null && Bookings.Count > 0;

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
