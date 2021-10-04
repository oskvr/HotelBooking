using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models.Wrappers
{
	[AddINotifyPropertyChangedInterface]
    public class BookingExtraWrapper
    {
		public BookingExtra BookingExtra { get; set; }
		public bool IsSelected { get; set; }
	}
}
