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
using HotelBooking.Presentation.Views;
using HotelBooking.Presentation.Utils;
using HotelBooking.Domain.Shared;

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
		private readonly IHotelService hotelService;
		private readonly IRegionManager regionManager;

		public int TotalDays => (Booking.CheckInDate.AddDays(Booking.LengthInDays) - Booking.CheckInDate).Days;
		public double? TotalPrice => Booking.RoomType is not null ? (Booking.RoomType.PricePerNight * TotalDays) + BookingExtras.Where(extra => extra.IsSelected).Sum(extra => extra.BookingExtra.Cost) : 0;

		public GlobalStore Store { get; }

		public BookingCreateViewModel(IBookingService bookingService, IHotelService hotelService, HotelBookingDbContext dbContext, IRegionManager regionManager, GlobalStore store)
		{
			CreateBookingCommand = new DelegateCommand(OnCreateBooking, CanCreatebooking);
			BookingUpdatedCommand = new DelegateCommand(OnBookingUpdate);
			this.bookingService = bookingService;
			this.dbContext = dbContext;
			AvailableBookingLengths = new ObservableCollection<int> { 7, 14 };
			this.hotelService = hotelService;
			this.regionManager = regionManager;
			Store = store;
			LoadDataAsync();
		}

		private bool CanCreatebooking()
		{
			return Booking.RoomType is not null;
		}

		private void OnBookingUpdate()
		{
			RaisePropertyChanged(nameof(TotalPrice));
			CreateBookingCommand.RaiseCanExecuteChanged();
		}
		private async void OnCreateBooking()
		{
			Booking.BookingExtras = BookingExtras;
			Booking.HotelId = HotelId;
			try
			{
				var createdBooking = await bookingService.Create(Booking);
				Booking = new BookingWrapper();
				var navigationParams = new NavigationParameters();
				navigationParams.Add("Booking", createdBooking);
				regionManager.RequestNavigate(RegionNames.CONTENT_REGION, nameof(BookingConfirmed), navigationParams);
				
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

			// TODO: Temporary for refreshing roomtypes
			LoadRoomTypesAsync();
		}
		private async void LoadDataAsync()
		{
			await LoadBookingExtrasAsync();
			await LoadRoomTypesAsync();
		}
		private async Task LoadBookingExtrasAsync()
		{
			var bookingExtras = await dbContext.BookingExtras.ToListAsync();
			BookingExtras.Clear();
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
			//var roomTypes = await dbContext.RoomTypes.ToListAsync();
			var roomTypes = await hotelService.GetAvailableRoomTypesBetweenDates(HotelId, Booking.CheckInDate, Booking.CheckOutDate);
			RoomTypes.Clear();
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
