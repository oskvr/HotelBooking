using HotelBooking.Domain.Shared;
using HotelBooking.Presentation.Utils;
using HotelBooking.Presentation.Views;
using ModernWpf.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace HotelBooking.Presentation.ViewModels
{

	public class MainWindowViewModel : BindableBase, INavigationAware
	{
		private DelegateCommand goBackCommand;
		public DelegateCommand GoBackCommand => goBackCommand ??= new DelegateCommand(ExecuteGoBackCommand, CanGoBack);
		void ExecuteGoBackCommand()
		{
			GoBack();
		}
		private NavigationViewItem selectedItem;
		private readonly IRegionManager regionManager;

		public GlobalStore Store { get; set; }
		public NavigationViewItem SelectedItem
		{
			get { return selectedItem; }
			set
			{
				SetProperty(ref selectedItem, value);
				if (selectedItem.Name == "Logout")
				{
					OnLogout();
					return;
				}
				regionManager.RequestNavigate(RegionNames.CONTENT_REGION, selectedItem.Name);
			}
		}

		public MainWindowViewModel(IRegionManager regionManager, GlobalStore store)
		{
			this.regionManager = regionManager;
			Store = store;
		}

		private async void OnLogout()
		{
			ContentDialog locationPromptDialog = new ContentDialog
			{
				Title = "Är du säker på att du vill logga ut?",
				PrimaryButtonText = "Logga ut",
				CloseButtonText = "Avbryt",
			};
			ContentDialogResult result = await locationPromptDialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				Store.CurrentUser = null;

				// Prevent going back after logging out
				regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.Clear();
				regionManager.RequestNavigate(RegionNames.CONTENT_REGION, nameof(HotelsOverview));
			}
		}

		private void GoBack()
		{
			if (regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.CanGoBack)
			{
				regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.GoBack();
			}
		}

		private bool CanGoBack()
		{
			//return true;
			return regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService.Journal.CanGoBack;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			regionManager.Regions[RegionNames.CONTENT_REGION].NavigationService = navigationContext.NavigationService;
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}
	}
}
