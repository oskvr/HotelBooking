using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.Events
{
    public record DateFilter(DateTime CheckInDate, int LengthInDays);
    public class DateFilterUpdatedEvent:PubSubEvent<DateFilter>
    {
        
    }
}
