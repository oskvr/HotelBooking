using HotelBooking.DAL.Services;
using HotelBooking.Presentation.Utils;
using HotelBooking.Presentation.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelBooking.Presentation.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class UserWrapper
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

	}
	public class RegisterViewModel : BindableBase, INavigationAware
	{
		private UserWrapper user = new();

		public UserWrapper User
		{
			get { return user; }
			set { SetProperty(ref user, value); RegisterCommand.RaiseCanExecuteChanged(); }
		}

		public string Email { get; set; }
		//public string FirstName { get; set; }
		//public string LastName { get; set; }
		private string firstName;
		public string FirstName
		{
			get { return firstName; }
			set { SetProperty(ref firstName, value); RegisterCommand.RaiseCanExecuteChanged(); }
		}
		private string lastName;
		public string LastName
		{
			get { return lastName; }
			set { SetProperty(ref lastName, value); RegisterCommand.RaiseCanExecuteChanged(); }
		}
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public DelegateCommand RegisterCommand { get; set; }

		private readonly IAuthenticationService authenticationService;
		private readonly IRegionManager regionManager;

		public RegisterViewModel(IAuthenticationService authenticationService, IRegionManager regionManager)
		{
			RegisterCommand = new DelegateCommand(OnRegister, CanRegister);
			this.authenticationService = authenticationService;
			this.regionManager = regionManager;
		}

		private async void OnRegister()
		{
			var result = await authenticationService.Register(Email, FirstName, LastName, Password, ConfirmPassword);
			if (result.IsSuccess)
			{
				await authenticationService.Login(Email, Password);

				// Prevent going back after registration and login
				regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.Clear();
				regionManager.RequestNavigate(RegionNames.CONTENT_REGION, nameof(HotelsOverview));
				Email = "";
				FirstName = "";
				LastName = "";
				Password = "";
				ConfirmPassword = "";
			}
			else
			{
				MessageBox.Show("Registreringen misslyckades");
			}
		}

		private bool CanRegister()
		{
			return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
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
