using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using HotelBooking.Presentation.Views;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
	public class HotelsOverviewViewModel : BindableBase
	{
		public ObservableCollection<Hotel> Hotels { get; set; }
		public ObservableCollection<RoomType> RoomTypes { get; set; }
		public DelegateCommand TogglePaneCommand { get; set; }
		public DelegateCommand<Hotel> NavigateToBookingCommand { get; set; }

		private GlobalStore store;
		private readonly HotelBookingDbContext dbContext;
		private readonly IRegionManager regionManager;

		public HotelsOverviewViewModel(HotelBookingDbContext dbContext, IRegionManager regionManager, GlobalStore store)
		{
			this.dbContext = dbContext;
			NavigateToBookingCommand = new DelegateCommand<Hotel>(OnNavigateToBooking);
			LoadInitData();
			this.regionManager = regionManager;
			this.store = store;
		}

		private void OnNavigateToBooking(Hotel hotel)
		{
			var navigationParams = new NavigationParameters();
			navigationParams.Add("hotelId", hotel.Id);
			string redirectView = store.IsLoggedIn ? nameof(BookingCreate) : nameof(Login);
			regionManager.RequestNavigate("ContentRegion", redirectView, navigationParams);
		}

		public async Task<ObservableCollection<RoomType>> GetRoomTypes()
		{
			var roomTypes = await dbContext.RoomTypes.ToListAsync();
			return new ObservableCollection<RoomType>(roomTypes);
		}
		public async Task<ObservableCollection<Hotel>> GetHotels()
		{
			var hotels = await dbContext.Hotels.Include(hotel => hotel.Ratings).ToListAsync();
			return new ObservableCollection<Hotel>(hotels);
		}
		private async void LoadInitData()
		{
			RoomTypes = await GetRoomTypes();
			Hotels = await GetHotels();
		}
	}
}




//namespace HotelBooking.Presentation.ViewModels
//{
//	public class HotelsOverviewViewModel : BindableBase
//	{
//		public ObservableCollection<Hotel> Hotels { get; set; }
//		public ObservableCollection<RoomType> RoomTypes { get; set; }
//		public Hotel SelectedHotel { get; set; }
//		public DelegateCommand TogglePaneCommand { get; set; }
//		public DelegateCommand NavigateToBookingCommand { get; set; }

//		public bool ShowPane { get; set; }
//		private GlobalStore store;
//		private readonly HotelBookingDbContext dbContext;
//		private readonly IRegionManager regionManager;

//		public HotelsOverviewViewModel(HotelBookingDbContext dbContext, IRegionManager regionManager, GlobalStore store)
//		{
//			this.dbContext = dbContext;
//			NavigateToBookingCommand = new DelegateCommand(OnNavigateToBooking);
//			LoadInitData();
//			this.regionManager = regionManager;
//			this.store = store;
//		}

//		private void OnNavigateToBooking()
//		{
//			var navigationParams = new NavigationParameters();
//			navigationParams.Add("hotelId", SelectedHotel.Id);
//			string redirectView = store.IsLoggedIn ? nameof(BookingCreate) : nameof(Login);
//			regionManager.RequestNavigate("ContentRegion", redirectView, navigationParams);
//		}

//		public async Task<ObservableCollection<RoomType>> GetRoomTypes()
//		{
//			var roomTypes = await dbContext.RoomTypes.ToListAsync();
//			return new ObservableCollection<RoomType>(roomTypes);
//		}
//		public async Task<ObservableCollection<Hotel>> GetHotels()
//		{
//			var hotels = await dbContext.Hotels.Include(hotel => hotel.Ratings).ToListAsync();
//			return new ObservableCollection<Hotel>(hotels);
//		}
//		private async void LoadInitData()
//		{
//			RoomTypes = await GetRoomTypes();
//			Hotels = await GetHotels();
//		}
//	}
//}
