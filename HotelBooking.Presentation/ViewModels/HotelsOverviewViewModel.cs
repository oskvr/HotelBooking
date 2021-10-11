﻿using HotelBooking.DAL.Data;
using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using HotelBooking.Presentation.Utils;
using HotelBooking.Presentation.Views;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HotelBooking.Presentation.ViewModels
{

	public record SortOption(string Name, string MappingProperty, ListSortDirection SortDirection);

	public class HotelsOverviewViewModel : BindableBase
	{
		public ObservableCollection<Hotel> Hotels { get; set; }
		public ICollectionView FilteredHotels { get; set; }
		public ObservableCollection<SortOption> SortOptions { get; set; }
		public ObservableCollection<RoomType> RoomTypes { get; set; }
		public DelegateCommand TogglePaneCommand { get; set; }
		public DelegateCommand<Hotel> NavigateToBookingCommand { get; set; }
		private SortOption selectedSortOption;
		public SortOption SelectedSortOption
		{
			get { return selectedSortOption; }
			set
			{
				SetProperty(ref selectedSortOption, value);
				if (FilteredHotels is not null)
				{
					FilteredHotels.SortDescriptions.Clear();
					FilteredHotels.SortDescriptions.Add(new SortDescription(selectedSortOption.MappingProperty, selectedSortOption.SortDirection));
					FilteredHotels.Refresh();
				}
			}
		}
		private GlobalStore store;
		private readonly HotelBookingDbContext dbContext;
		private readonly IRegionManager regionManager;
		private readonly IHotelService hotelService;

		public HotelsOverviewViewModel(HotelBookingDbContext dbContext, IRegionManager regionManager, GlobalStore store, IHotelService hotelService)
		{
			this.dbContext = dbContext;
			this.hotelService = hotelService;
			NavigateToBookingCommand = new DelegateCommand<Hotel>(OnNavigateToBooking);
			LoadInitData();
			this.regionManager = regionManager;
			this.store = store;
			SortOptions = new ObservableCollection<SortOption>
			{
				new SortOption("Pris (fallande)", "PricePerNight", ListSortDirection.Descending),
				new SortOption("Pris (stigande)", "PricePerNight", ListSortDirection.Ascending),
				new SortOption("Hotelklass (fallande)", "Rating", ListSortDirection.Descending),
				new SortOption("Hotelklass (stigande)", "Rating", ListSortDirection.Ascending),
				new SortOption("Tillgänglighet", "IsAvailable", ListSortDirection.Ascending),
				new SortOption("Namn (stigande)", "Name", ListSortDirection.Ascending),
				new SortOption("Namn (fallande)", "Name", ListSortDirection.Descending),
			};
		}

		private void OnNavigateToBooking(Hotel hotel)
		{
			var navigationParams = new NavigationParameters();
			navigationParams.Add("hotelId", hotel.Id);
			string redirectView = store.IsLoggedIn ? nameof(BookingCreate) : nameof(Login);
			regionManager.RequestNavigate(RegionNames.CONTENT_REGION, redirectView, navigationParams);
		}

		public async Task<ObservableCollection<RoomType>> GetRoomTypes()
		{
			var roomTypes = await dbContext.RoomTypes.ToListAsync();
			return new ObservableCollection<RoomType>(roomTypes);
		}
		public async Task<ObservableCollection<Hotel>> GetHotels()
		{
			var hotels = await hotelService.GetAll();
			return new ObservableCollection<Hotel>(hotels);
		}
		private async void LoadInitData()
		{
			RoomTypes = await GetRoomTypes();
			Hotels = await GetHotels();
			FilteredHotels = CollectionViewSource.GetDefaultView(Hotels);
			FilteredHotels.Filter = new Predicate<object>(ItemFilter);
			FilteredHotels.Refresh();
		}
		private bool ItemFilter(object obj)
		{
			if (obj is Hotel hotel)
			{
				return true;
			}
			return false;
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
