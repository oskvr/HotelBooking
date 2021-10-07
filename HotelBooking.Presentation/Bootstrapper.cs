using HotelBooking.DAL.Data;
using HotelBooking.DAL.Services;
using HotelBooking.Domain.Shared;
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
			containerRegistry.RegisterScoped<HotelBookingDbContext>();
			containerRegistry.Register<IHotelService, HotelService>();
			containerRegistry.Register<IBookingService, BookingService>();
			containerRegistry.Register<IAuthenticationService, AuthenticationService>();
			containerRegistry.RegisterSingleton<GlobalStore>();
			containerRegistry.RegisterForNavigation<Login>();
			containerRegistry.RegisterForNavigation<Register>();
			containerRegistry.RegisterForNavigation<HotelsOverview>();
			containerRegistry.RegisterForNavigation<BookingCreate>();
			containerRegistry.RegisterForNavigation<BookingsList>();

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
