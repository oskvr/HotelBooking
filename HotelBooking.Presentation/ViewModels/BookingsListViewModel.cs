﻿using HotelBooking.DAL.Services;
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

namespace HotelBooking.Presentation.ViewModels
{
	public class BookingsListViewModel : BindableBase, INavigationAware
	{
		public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();
		public bool HasBookings => Bookings is not null && Bookings.Count > 0;
		public Booking SelectedBooking { get; set; }
		private readonly IBookingService bookingService;
		public DelegateCommand<Booking> DeleteBookingCommand => new DelegateCommand<Booking>(OnDeleteBooking);

		public BookingsListViewModel(IBookingService bookingService)
		{
			this.bookingService = bookingService;
		}
		async void OnDeleteBooking(Booking booking)
		{
			await bookingService.Delete(booking.Id);
			Bookings.Remove(booking);
			Debug.WriteLine("Deleted booking with id: " + booking.Id);
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
			RaisePropertyChanged(nameof(HasBookings));
		}
		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			LoadBookings();
		}
	}
}
