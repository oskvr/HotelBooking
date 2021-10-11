using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using HotelBooking.Presentation.Utils;
using ModernWpf.Controls;
using Prism.Regions;

namespace HotelBooking.Presentation.Views
{
	public partial class MainWindow
	{
		private readonly IRegionManager regionManager;

		public MainWindow(IRegionManager regionManager)
		{
			InitializeComponent();
			this.regionManager = regionManager;
		}

        private void MainNav_Navigate(NavigationViewItem item)
        {
            //switch (item.Tag)
            //{
            //    case "Test":
            //        ContentFrame.Navigate(nameof(Login));
            //        break;
            //}
        }

		private void MainNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			Debug.WriteLine(sender.MenuItems);
			//ContentFrame.Navigate(typeof(Login));

		}

		private void MainNavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
		{
			regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.GoBack();	
		}
	}
}
