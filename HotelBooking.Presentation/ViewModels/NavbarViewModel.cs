using HotelBooking.Presentation.Views;
using ModernWpf.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
    public class NavbarViewModel:BindableBase
    {
		private NavigationViewItem selectedItem;
		private readonly IRegionManager regionManager;
		public NavigationViewItem SelectedItem
		{
			get { return selectedItem; }
			set
			{
				SetProperty(ref selectedItem, value);
				regionManager.RequestNavigate("ContentRegion", selectedItem.Name);
			}
		}


		public NavbarViewModel(IRegionManager regionManager)
		{
			this.regionManager = regionManager;
		}

	}
}
