using HotelBooking.Domain.Shared;
using HotelBooking.Presentation.Views;
using ModernWpf.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace HotelBooking.Presentation.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private NavigationViewItem selectedItem;
		private readonly IRegionManager regionManager;
		public GlobalStore Store { get; set; }
		public NavigationViewItem SelectedItem
		{
			get { return selectedItem; }
			set
			{
				SetProperty(ref selectedItem, value);
				if (selectedItem.Name == "Logout")
				{
					OnLogout();
					return;
				}
				regionManager.RequestNavigate("ContentRegion", selectedItem.Name);
			}
		}

		private void OnLogout()
		{
			Store.CurrentUser = null;
			regionManager.RequestNavigate("ContentRegion", nameof(Login));
		}

		public MainWindowViewModel(IRegionManager regionManager, GlobalStore store)
		{
			this.regionManager = regionManager;
			Store = store;
		}
	}
}
