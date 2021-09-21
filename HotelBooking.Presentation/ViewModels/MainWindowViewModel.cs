using Prism.Mvvm;

namespace HotelBooking.Presentation.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _title = "ModernWpf Application";
		public bool IsUserLoggedIn { get; set; } = false;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public MainWindowViewModel()
		{

		}
	}
}
