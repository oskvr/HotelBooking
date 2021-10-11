using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using HotelBooking.Presentation.Utils;
using HotelBooking.Presentation.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelBooking.Presentation.ViewModels
{
	public class LoginViewModel : BindableBase, INavigationAware
	{
		public string Email { get; set; }
		public string Password { private get; set; }
		public DelegateCommand LoginCommand { get; set; }
		private readonly IAuthenticationService authenticationService;
		private readonly IRegionManager regionManager;
		private NavigationParameters navigationParams;
		private string redirectView;

		public LoginViewModel(IAuthenticationService authenticationService, IRegionManager regionManager)
		{
			this.authenticationService = authenticationService;
			LoginCommand = new DelegateCommand(OnLogin);
			this.regionManager = regionManager;
		}

		private async void OnLogin()
		{
			LoginResult result = await authenticationService.Login(Email, Password);
			if (result.IsSuccess)
			{
				// Prevent going back after logging in
				regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.Clear();
				
				regionManager.RequestNavigate(RegionNames.CONTENT_REGION, redirectView, navigationParams);
				Email = "";
				Password = "";
			}
			else
			{
				MessageBox.Show("Login failed");
			}
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
			// TODO: ---- FOR TESTING, REMOVE ----
			Email = "oskar@gmail.com";
			Password = "oskar";
			OnLogin();
			// TODO: ---- FOR TESTING, REMOVE ----

			navigationParams = new NavigationParameters();

			bool hasHotelId = navigationContext.Parameters.Any(param => param.Key == "hotelId");
			if (hasHotelId)
			{
				// If hotelId exists the user wanted to book a hotel
				int hotelId = navigationContext.Parameters.GetValue<int>("hotelId");
				navigationParams.Add("hotelId", hotelId);
				// Redirect the user to the booking page on successful login
				redirectView = nameof(BookingCreate);
			}
			else
			{
				// Do a normal login and redirect to "home" page
				redirectView = nameof(HotelsOverview);
			}
		}
	}
}
