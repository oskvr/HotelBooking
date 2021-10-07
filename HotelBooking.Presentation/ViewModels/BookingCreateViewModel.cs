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
using HotelBooking.Domain.Models.Wrappers;
using System.Windows;

namespace HotelBooking.Presentation.ViewModels
{
	public class BookingCreateViewModel : BindableBase, INavigationAware
	{
		public BookingWrapper Booking { get; set; } = new BookingWrapper();
		public int HotelId { get; set; }
		public ObservableCollection<RoomType> RoomTypes { get; set; } = new ObservableCollection<RoomType>();
		public ObservableCollection<BookingExtraWrapper> BookingExtras { get; set; } = new ObservableCollection<BookingExtraWrapper>();
		public ObservableCollection<int> AvailableBookingLengths { get; set; }
		public DelegateCommand BookingUpdatedCommand { get; set; }
		public DelegateCommand CreateBookingCommand { get; set; }
		private readonly IBookingService bookingService;
		private readonly HotelBookingDbContext dbContext;

		public int TotalDays => (Booking.CheckInDate.AddDays(Booking.LengthInDays) - Booking.CheckInDate).Days;
		public double? TotalPrice => (Booking.RoomType.PricePerNight * TotalDays) + BookingExtras.Where(extra => extra.IsSelected).Sum(extra => extra.BookingExtra.Cost);
		public BookingCreateViewModel(IBookingService bookingService, HotelBookingDbContext dbContext)
		{
			CreateBookingCommand = new DelegateCommand(OnCreateBooking);
			BookingUpdatedCommand = new DelegateCommand(OnBookingUpdate);
			this.bookingService = bookingService;
			this.dbContext = dbContext;
			LoadDataAsync();
			AvailableBookingLengths = new ObservableCollection<int> { 7, 14 };
		}
		private void OnBookingUpdate()
		{
			RaisePropertyChanged(nameof(TotalPrice));
		}
		private async void OnCreateBooking()
		{
			Booking.BookingExtras = BookingExtras;
			Booking.HotelId = HotelId;
			try
			{
				await bookingService.Create(Booking);
				Booking = new BookingWrapper();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
		private async void LoadDataAsync()
		{
			await LoadBookingExtrasAsync();
			await LoadRoomTypesAsync();
		}
		private async Task LoadBookingExtrasAsync()
		{
			var bookingExtras = await dbContext.BookingExtras.ToListAsync();
			foreach (BookingExtra extra in bookingExtras)
			{
				BookingExtraWrapper bookingExtra = new()
				{
					BookingExtra = extra,
				};
				BookingExtras.Add(bookingExtra);
			}
		}

		private async Task LoadRoomTypesAsync()
		{
			var roomTypes = await dbContext.RoomTypes.ToListAsync();
			foreach (var roomType in roomTypes)
			{
				RoomTypes.Add(roomType);
			}
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
			var hotelId = navigationContext.Parameters.GetValue<int>("hotelId");
			Booking = new BookingWrapper();
			HotelId = hotelId;
		}
	}
}
