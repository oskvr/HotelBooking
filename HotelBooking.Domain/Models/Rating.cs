using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public class Rating : BaseEntity
	{
		public double Score { get; set; }
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int HotelId { get; set; }
		public Hotel Hotel { get; set; }
	}
}
