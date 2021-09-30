using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
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

namespace HotelBooking.Presentation.ViewModels
{
	public class LoginViewModel : BindableBase, INavigationAware
	{
		public string Email { get; set; }
		public string Password { private get; set; }
		public DelegateCommand LoginCommand { get; set; }
		private readonly IAuthenticationService authenticationService;
		private readonly IRegionManager regionManager;

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
				regionManager.RequestNavigate("ContentRegion", nameof(HotelsOverview));
				Debug.WriteLine($"{result.User.FirstName} {result.User.LastName} logged in");
				Email = "";
				Password = "";
			}
			else
			{
				Debug.WriteLine("Login failed");
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
			Debug.WriteLine("Navigated to login");
		}
	}
}
