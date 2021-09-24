using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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

		private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
			var item = args.SelectedItemContainer as NavigationViewItem;
			Debug.WriteLine(item.Tag);
			//switch (item.Tag)
			//{
			//	case nameof(HotelsOverview):
			//		regionManager.RequestNavigate("ContentRegion", nameof(HotelsOverview));
			//		break;
			//	case nameof(Login):
			//		regionManager.RequestNavigate("ContentRegion", nameof(Login));
			//		break;
			//	default:
			//		Debug.WriteLine("Default switch");
			//		break;
			//}
		}
	}
}
