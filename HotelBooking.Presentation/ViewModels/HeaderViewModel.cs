using HotelBooking.Presentation.Events;
using HotelBooking.Presentation.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.ViewModels
{
	public class HeaderViewModel : BindableBase
	{
		public int LengthInDays { get; set; } = Bookings.AVAILABLE_BOOKING_LENGTHS[0];
		public DateTime CheckIndate { get; set; } = DateTime.Now;
		private DelegateCommand dateFilterUpdatedCommand;
		private readonly IEventAggregator eventAggregator;

		public DelegateCommand DateFilterUpdatedCommand => dateFilterUpdatedCommand ??= new DelegateCommand(OnDateFilterUpdated);

		public HeaderViewModel(IEventAggregator eventAggregator)
		{
			this.eventAggregator = eventAggregator;
		}
		void OnDateFilterUpdated()
		{
			eventAggregator.GetEvent<DateFilterUpdatedEvent>().Publish(new DateFilter(CheckIndate, LengthInDays));
		}
	}
}
