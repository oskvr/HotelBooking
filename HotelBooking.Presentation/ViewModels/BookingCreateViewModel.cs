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
		public DelegateCommand BookingUpdatedCommand { get; set; }
		public DelegateCommand CreateBookingCommand { get; set; }
		private readonly IBookingService bookingService;
		private readonly HotelBookingDbContext dbContext;
		private readonly IHotelService hotelService;
		private readonly IRegionManager regionManager;

		public GlobalStore Store { get; }

		public BookingCreateViewModel(IBookingService bookingService, IHotelService hotelService, HotelBookingDbContext dbContext, IRegionManager regionManager, GlobalStore store)
		{
			CreateBookingCommand = new DelegateCommand(OnCreateBooking, CanCreatebooking);
			BookingUpdatedCommand = new DelegateCommand(OnBookingUpdate);
			this.bookingService = bookingService;
			this.dbContext = dbContext;
			this.hotelService = hotelService;
			this.regionManager = regionManager;
			Store = store;
		}

		private bool CanCreatebooking()
		{
			return !string.IsNullOrEmpty(Booking.RoomType.Type);
		}

		private void OnBookingUpdate()
		{
			RaisePropertyChanged(nameof(Booking));
			CreateBookingCommand.RaiseCanExecuteChanged();
		}
		private async void OnCreateBooking()
		{
			try
			{
				Booking.HotelId = HotelId;
				var createdBooking = await bookingService.Create(Booking);
				var navigationParams = new NavigationParameters();
				navigationParams.Add("Booking", createdBooking);
				regionManager.RequestNavigate(RegionNames.CONTENT_REGION, nameof(BookingConfirmed), navigationParams);

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
			BookingExtras.Clear();
			foreach (BookingExtra extra in bookingExtras)
			{
				BookingExtraWrapper bookingExtra = new()
				{
					BookingExtra = extra,
				};
				Booking.BookingExtras.Add(bookingExtra);
			}
		}

		private async Task LoadRoomTypesAsync()
		{
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
			//Booking.CheckInDate = CheckInDate;
			//Booking.LengthInDays = LengthInDays;
			LoadDataAsync();
			HotelId = hotelId;
		}
	}
}
