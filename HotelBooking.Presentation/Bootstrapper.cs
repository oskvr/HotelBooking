using HotelBooking.DAL.Data;
using HotelBooking.DAL.Services;
using HotelBooking.Domain.Shared;
using HotelBooking.Presentation.Utils;
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

			// Navigation
			containerRegistry.RegisterForNavigation<Login>();
			containerRegistry.RegisterForNavigation<Register>();
			containerRegistry.RegisterForNavigation<HotelsOverview>();
			containerRegistry.RegisterForNavigation<BookingCreate>();
			containerRegistry.RegisterForNavigation<BookingsList>();
			containerRegistry.RegisterForNavigation<BookingConfirmed>();

		}
		protected override void OnInitialized()
		{
			base.OnInitialized();
			IRegionManager regionManager = Container.Resolve<IRegionManager>();

			IRegion contentRegion = regionManager.Regions[RegionNames.CONTENT_REGION];
			
			HotelsOverview hotelsOverview = Container.Resolve<HotelsOverview>();

			regionManager.RegisterViewWithRegion("HeaderRegion", typeof(Header));
			contentRegion.Add(hotelsOverview);

		}
	}
}
