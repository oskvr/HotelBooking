using HotelBooking.DAL.Services;
using HotelBooking.Presentation.Views;
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
	public class RegisterViewModel : BindableBase, INavigationAware
	{
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public DelegateCommand RegisterCommand { get; set; }

		private readonly IAuthenticationService authenticationService;
		private readonly IRegionManager regionManager;

		public RegisterViewModel(IAuthenticationService authenticationService, IRegionManager regionManager)
		{
			RegisterCommand = new DelegateCommand(OnRegister);
			this.authenticationService = authenticationService;
			this.regionManager = regionManager;
		}

		private async void OnRegister()
		{
			var result = await authenticationService.Register(Email, FirstName, LastName, Password, ConfirmPassword);
			if (result.IsSuccess)
			{
				await authenticationService.Login(Email, Password);
				regionManager.RequestNavigate("ContentRegion", nameof(HotelsOverview));
				Email = "";
				FirstName = "";
				LastName = "";
				Password = "";
				ConfirmPassword = "";
				Debug.WriteLine($"User id: {result.User.Id} Email: {result.User.Email}");
			}
			else
			{
				Debug.WriteLine("Registration failed");
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
			
		}
	}
}
