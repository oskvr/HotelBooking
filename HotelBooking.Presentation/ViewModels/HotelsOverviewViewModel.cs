using HotelBooking.DAL.Data;
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HotelBooking.Presentation.ViewModels
{

	public record SortOption(string Name, string MappingProperty, ListSortDirection SortDirection);
	public record HotelViewModel(int Id, string Name, string City, double Rating, string ImageUrl, List<RoomType> AvailableRoomTypes)
	{
		public bool IsAvailable => AvailableRoomTypes.Count > 0;
		public int FromPrice => AvailableRoomTypes.Count == 0 ? 0 : AvailableRoomTypes.OrderBy(roomType => roomType.PricePerNight).FirstOrDefault().PricePerNight;
		public Visibility Visibility => IsAvailable ? Visibility.Visible : Visibility.Hidden;
	}
	public class HotelsOverviewViewModel : BindableBase, INavigationAware
	{
		public DateTime CheckInDateFilter { get; set; } = DateTime.Now;
		public int LengthInDaysFilter { get; set; } = Bookings.AVAILABLE_BOOKING_LENGTHS[0];
		private DelegateCommand dateFilterUpdatedCommand;
		public DelegateCommand DateFilterUpdatedCommand => dateFilterUpdatedCommand ??= new DelegateCommand(ExecuteDateFilterUpdatedCommand);

		void ExecuteDateFilterUpdatedCommand()
		{
			LoadInitData();
		}

		public ObservableCollection<Hotel> Hotels { get; set; }
		public ICollectionView FilteredHotels { get; set; }
		public ObservableCollection<SortOption> SortOptions { get; set; }
		public DelegateCommand<object> NavigateToBookingCommand { get; set; }
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
			NavigateToBookingCommand = new DelegateCommand<object>(OnNavigateToBooking);
			LoadInitData();
			this.regionManager = regionManager;
			this.store = store;
			SortOptions = new ObservableCollection<SortOption>
			{
				new SortOption("Pris (fallande)", "FromPrice", ListSortDirection.Descending),
				new SortOption("Pris (stigande)", "FromPrice", ListSortDirection.Ascending),
				new SortOption("Hotelklass (fallande)", "Rating", ListSortDirection.Descending),
				new SortOption("Hotelklass (stigande)", "Rating", ListSortDirection.Ascending),
				new SortOption("Namn (stigande)", "Name", ListSortDirection.Ascending),
				new SortOption("Namn (fallande)", "Name", ListSortDirection.Descending),
			};
		}

		private void OnNavigateToBooking(object hotelId)
		{
			var navigationParams = new NavigationParameters();
			navigationParams.Add("hotelId", (int)hotelId);

			// TODO: Temporary fix to store selected date period from HotelsOverview => BookingCreate
			SelectedDate.CHECK_IN_DATE = CheckInDateFilter;
			SelectedDate.LENGTH_IN_DAYS = LengthInDaysFilter;

			string redirectView = store.IsLoggedIn ? nameof(BookingCreate) : nameof(Login);
			regionManager.RequestNavigate(RegionNames.CONTENT_REGION, redirectView, navigationParams);
		}
		private async void LoadInitData()
		{
			var hotels = await hotelService.GetAll();
			var hotelViewModels = new List<HotelViewModel>();
			foreach (var hotel in hotels)
			{
				var availableRoomTypes = await hotelService.GetAvailableRoomTypesBetweenDates(hotel.Id, CheckInDateFilter, CheckInDateFilter.AddDays(LengthInDaysFilter));
				var converted = new HotelViewModel(hotel.Id, hotel.Name, hotel.City, hotel.Rating, hotel.Image.ToString(), availableRoomTypes);
				hotelViewModels.Add(converted);
			}
			FilteredHotels = CollectionViewSource.GetDefaultView(hotelViewModels);
			FilteredHotels.Filter = new Predicate<object>(ItemFilter);
			if (SelectedSortOption is not null)
			{
				FilteredHotels.SortDescriptions.Clear();
				FilteredHotels.SortDescriptions.Add(new SortDescription(SelectedSortOption.MappingProperty, SelectedSortOption.SortDirection));
			}
			FilteredHotels.Refresh();
		}
		private bool ItemFilter(object obj)
		{
			if (obj is HotelViewModel hotel)
			{
				return hotel.IsAvailable;
			}
			return false;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			LoadInitData();
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}
	}
}



#region old_code_delete_anytime

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
#endregion