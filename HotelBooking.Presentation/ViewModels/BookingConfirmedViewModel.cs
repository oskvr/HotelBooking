using HotelBooking.Domain.Models;
using HotelBooking.Presentation.Utils;
using HotelBooking.Presentation.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
    public class BookingConfirmedViewModel:BindableBase, INavigationAware
    {
		public Booking Booking { get; set; } = new Booking();
		private DelegateCommand navigateToBookingsCommand;
		private readonly IRegionManager regionManager;

		public DelegateCommand NavigateToBookingsCommand => navigateToBookingsCommand ??= new DelegateCommand(ExecuteNavigateToBookingsCommand);
		public BookingConfirmedViewModel(IRegionManager regionManager)
		{
			this.regionManager = regionManager;
		}
		void ExecuteNavigateToBookingsCommand()
		{
			regionManager.RequestNavigate(RegionNames.CONTENT_REGION, nameof(BookingsList));
		}
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Booking = navigationContext.Parameters.GetValue<Booking>("Booking");

			// Clear journal to prevent going back to completed booking
			regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.Clear();
		}
	}
}
