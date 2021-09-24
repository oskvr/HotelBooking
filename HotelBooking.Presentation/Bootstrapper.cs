using HotelBooking.Presentation.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace HotelBooking.Presentation
{
	public class Bootstrapper : PrismBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<Login>();
			containerRegistry.RegisterForNavigation<HotelsOverview>();

		}
		protected override void OnInitialized()
		{
			base.OnInitialized();
			var regionManager = Container.Resolve<IRegionManager>();
			var contentRegion = regionManager.Regions["ContentRegion"];
			//var loginView = Container.Resolve<Login>();
			//contentRegion.Add(loginView);
			var hotelsOverview = Container.Resolve<HotelsOverview>();
			contentRegion.Add(hotelsOverview);

		}
	}
}
