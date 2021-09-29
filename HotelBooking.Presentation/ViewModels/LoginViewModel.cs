﻿using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
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
		public GlobalStore Store { get; set; }

		private readonly IAuthenticationService authenticationService;

		public LoginViewModel(IAuthenticationService authenticationService, GlobalStore store)
		{
			this.authenticationService = authenticationService;
			LoginCommand = new DelegateCommand(OnLogin);
			Store = store;
		}

		private async void OnLogin()
		{
			LoginResult result = await authenticationService.Login(Email, Password);
			if (result.IsSuccess)
			{
				Debug.WriteLine($"{result.User.FirstName} {result.User.LastName} logged in");
				Store.CurrentUser = result.User;
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
