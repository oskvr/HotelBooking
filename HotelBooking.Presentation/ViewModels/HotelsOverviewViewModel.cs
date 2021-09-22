using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
	public class Hotel
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string ImageUrl { get; set; }
		public int RatingScore { get; set; }
	}
	public class HotelsOverviewViewModel : BindableBase
	{
		public ObservableCollection<Hotel> Hotels { get; set; }
		public Hotel SelectedHotel { get; set; }
		public DelegateCommand TogglePaneCommand{ get; set; }
		public bool ShowPane { get; set; }
		public HotelsOverviewViewModel()
		{
			TogglePaneCommand = new DelegateCommand(() => ShowPane = !ShowPane);
			var random = new Random();
			Hotels = new()
			{
				new Hotel
				{
					Name = "Bay Area Hotel",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Neonside Hotel",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Pascha Bay",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Longbeach Hotel",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Hotel 66",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Scandic Stockholm",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Bay Area Hotel",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Neonside Hotel",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Pascha Bay",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Longbeach Hotel",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Hotel 66",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
				new Hotel
				{
					Name = "Scandic Stockholm",
					ImageUrl = "https://picsum.photos/500",
					Country = "Sweden",
					RatingScore = random.Next(0, 6)
				},
			};
		}
	}
}
