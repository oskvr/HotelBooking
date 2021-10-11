using HotelBooking.Domain.Models;
using HotelBooking.Presentation.ViewModels;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelBooking.Presentation.Views
{
	/// <summary>
	/// Interaction logic for BookingsList.xaml
	/// </summary>
	public partial class BookingsList : UserControl
	{
		public BookingsList()
		{
			InitializeComponent();
		}


		// Using code-behind for action clicks because binding to a command in deeply nested DataGrid sucks
		#region Click-events
		private void BtnEditBooking_Click(object sender, RoutedEventArgs e)
		{

		}
		private void BtnDeleteBooking_Click(object sender, RoutedEventArgs e)
		{
			var viewModel = (BookingsListViewModel)DataContext;
			if (viewModel is not null)
			{
				var button = (AppBarButton)sender;
				Booking bookingToDelete = button.CommandParameter as Booking;
				viewModel.DeleteBookingCommand.Execute(bookingToDelete);
			}
		}
		#endregion
	}
}
