using HotelBooking.DAL.Data;
using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;
using System.Collections.Specialized;

namespace HotelBooking.Presentation.ViewModels
{
	public class BookingExtraViewModel : BindableBase
	{
		public BookingExtra BookingExtra { get; set; }
		public bool IsSelected { get; set; }
	}
	public class BookingCreateViewModel : BindableBase, INavigationAware
	{
		public Hotel SelectedHotel { get; set; }
		public Booking Booking { get; set; }
		public ObservableCollection<BookingExtraViewModel> BookingExtras { get; set; } = new ObservableCollection<BookingExtraViewModel>();
		public DelegateCommand CreateBookingCommand { get; set; }
		private readonly IBookingService bookingService;
		private readonly HotelBookingDbContext dbContext;
		public BookingCreateViewModel(IBookingService bookingService, HotelBookingDbContext dbContext)
		{
			CreateBookingCommand = new DelegateCommand(OnCreateBooking);
			this.bookingService = bookingService;
			this.dbContext = dbContext;
			LoadBookingExtras();
		}

		private async void LoadBookingExtras()
		{
			var bookingExtras = await dbContext.BookingExtras.ToListAsync();
			foreach (var extra in bookingExtras)
			{
				BookingExtraViewModel vm = new()
				{
					BookingExtra = extra,
					IsSelected = false,
				};
				BookingExtras.Add(vm);
			}
		}

		private async void OnCreateBooking()
		{
			foreach (var item in BookingExtras.Where(be=>be.IsSelected))
			{
				Booking.BookingExtras.Add(item.BookingExtra);
			}
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
