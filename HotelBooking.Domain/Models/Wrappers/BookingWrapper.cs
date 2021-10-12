using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models.Wrappers
{
 	[AddINotifyPropertyChangedInterface]
	public class BookingWrapper
	{
		public DateTime CheckInDate { get; set; } = DateTime.Now;
		public int LengthInDays { get; set; } = 7;
		public DateTime CheckOutDate => CheckInDate.AddDays(LengthInDays);
		public string DisplayCheckInDate => CheckInDate.ToString("D",
		  CultureInfo.CreateSpecificCulture("sv-SE"));
		public string DisplayCheckOutDate => CheckOutDate.ToString("D",
				  CultureInfo.CreateSpecificCulture("sv-SE"));
		public ICollection<BookingExtraWrapper> BookingExtras { get; set; } = new ObservableCollection<BookingExtraWrapper>();
		public List<BookingExtra> SelectedBookingExtras => BookingExtras.Where(x => x.IsSelected).Select(x=>x.BookingExtra).ToList();
		public RoomType RoomType { get; set; } = new RoomType();
		public int HotelId { get; set; }

		// Computed properties
		//public int TotalDays => (CheckInDate.AddDays(LengthInDays) - CheckInDate).Days;
		public double? BookingExtrasSum => SelectedBookingExtras.Sum(extra => extra.Cost);
		public double? RoomTypeSum => RoomType is null ? null : RoomType.PricePerNight * LengthInDays;
		public double? TotalPrice => RoomType is null ? null : BookingExtrasSum + RoomTypeSum;

	}
}
