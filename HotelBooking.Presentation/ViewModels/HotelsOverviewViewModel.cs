using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
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
	public class MockHotel
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string ThumbnailImage { get; set; }
		public int Rating { get; set; }
	}

	public class HotelsOverviewViewModel : BindableBase
	{
		public ObservableCollection<Hotel> Hotels { get; set; }
		public ObservableCollection<RoomType> RoomTypes { get; set; }
		public Hotel SelectedHotel { get; set; }
		public DelegateCommand TogglePaneCommand { get; set; }
		public DelegateCommand NavigateToBookingCommand { get; set; }

		public bool ShowPane { get; set; }

		private readonly HotelBookingDbContext dbContext;
		private readonly IRegionManager regionManager;

		public HotelsOverviewViewModel(HotelBookingDbContext dbContext, IRegionManager regionManager)
		{
			this.dbContext = dbContext;
			NavigateToBookingCommand = new DelegateCommand(OnNavigateToBooking);
			LoadInitData();
			this.regionManager = regionManager;
		}

		private void OnNavigateToBooking()
		{
			var navigationParams = new NavigationParameters();
			navigationParams.Add("id", SelectedHotel.Id);
			regionManager.RequestNavigate("ContentRegion", nameof(BookingCreate), navigationParams);
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
		public ObservableCollection<MockHotel> GetMockHotels()
		{
			var random = new Random();
			return new ObservableCollection<MockHotel>()
			{
				new MockHotel
				{
					Name = "Bay Area Hotel",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Neonside Hotel",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Pascha Bay",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Longbeach Hotel",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Hotel 66",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Scandic Stockholm",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Bay Area Hotel",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Neonside Hotel",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Pascha Bay",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Longbeach Hotel",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Hotel 66",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
				new MockHotel
				{
					Name = "Scandic Stockholm",
					ThumbnailImage = "https://picsum.photos/500",
					Country = "Sweden",
					Rating = random.Next(0, 6)
				},
			};
		}
	}
}
