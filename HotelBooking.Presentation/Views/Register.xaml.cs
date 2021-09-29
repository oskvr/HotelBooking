using HotelBooking.Presentation.ViewModels;
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
	/// Interaction logic for Register.xaml
	/// </summary>
	public partial class Register : UserControl
	{
		public Register()
		{
			InitializeComponent();
		}

		private void RegisterConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (DataContext != null)
			{ ((RegisterViewModel)DataContext).ConfirmPassword = ((PasswordBox)sender).Password; }
		}

		private void RegisterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (DataContext != null)
			{ ((RegisterViewModel)DataContext).Password = ((PasswordBox)sender).Password; }
		}
	}
}
