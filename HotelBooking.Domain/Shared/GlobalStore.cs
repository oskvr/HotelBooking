using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Domain.Models;
using PropertyChanged;
namespace HotelBooking.Domain.Shared
{
    [AddINotifyPropertyChangedInterface]
    public class GlobalStore
    {
		public User CurrentUser { get; set; }
		public bool IsLoggedIn => CurrentUser is not null;
		public bool IsNotLoggedIn => CurrentUser is null; // Needed for binding inverted visibility in XAML
	}
}
