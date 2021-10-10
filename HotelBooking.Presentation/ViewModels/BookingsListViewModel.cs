using HotelBooking.DAL.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Prism.Commands;
using System.Diagnostics;
using ModernWpf.Controls;

namespace HotelBooking.Presentation.ViewModels
{
	public class BookingsListViewModel : BindableBase, INavigationAware
	{
		public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();
		public bool UserHasNoBookings => Bookings is null || Bookings.Count == 0;
		public Booking SelectedBooking { get; set; }
		private readonly IBookingService bookingService;
		public DelegateCommand<Booking> DeleteBookingCommand { get; }

		public BookingsListViewModel(IBookingService bookingService)
		{
			DeleteBookingCommand = new DelegateCommand<Booking>(OnDeleteBooking);
			this.bookingService = bookingService;
		}
		async void OnDeleteBooking(Booking booking)
		{
			ContentDialog locationPromptDialog = new ContentDialog
			{
				Title = "Är du säker på att du vill avboka?",
				Content = $"{booking.Hotel.Name}",
				PrimaryButtonText = "Avboka",
				CloseButtonText = "Avbryt",
			};

			ContentDialogResult result = await locationPromptDialog.ShowAsync();
			switch (result)
			{
				case ContentDialogResult.None:
					break;
				case ContentDialogResult.Primary:
					await bookingService.Delete(booking.Id);
					Bookings.Remove(booking);
					Debug.WriteLine("Deleted booking with id: " + booking.Id);
					RaisePropertyChanged(nameof(UserHasNoBookings));
					break;
				case ContentDialogResult.Secondary:
					break;
				default:
					break;
			}
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}
		private async void LoadBookings()
		{
			var bookings = await bookingService.GetAll();
			Bookings.Clear();
			foreach (var booking in bookings)
			{
				Bookings.Add(booking);
			}
			RaisePropertyChanged(nameof(UserHasNoBookings));
		}
		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			LoadBookings();
		}
	}
}
