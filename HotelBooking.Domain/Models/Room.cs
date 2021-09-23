using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
	public enum RoomTypeEnum
	{
		Single = 1,
		Double = 2,
		Triple = 3,
	}
	public abstract class Room : BaseEntity
	{
		public abstract int RoomTypeId { get; set; }
		public RoomType Type { get; set; }
		public abstract int MaxPeople { get; set; }
		public abstract double PricePerNight { get; set; }
		public ICollection<Booking> Bookings { get; set; }
		public int HotelId { get; set; }
		public Hotel Hotel { get; set; }

	}

	public class SingleRoom : Room
	{
		public override int RoomTypeId { get; set; } = (int)RoomTypeEnum.Single;
		public override int MaxPeople { get; set; } = 1;
		public override double PricePerNight { get; set; } = 80;
	}

	public class DoubleRoom : Room
	{
		public override int RoomTypeId { get; set; } = (int)RoomTypeEnum.Double;
		public override int MaxPeople { get; set; } = 2;
		public override double PricePerNight { get; set; } = 150;
	}

	public class TripleRoom : Room
	{
		public override int RoomTypeId { get; set; } = (int)RoomTypeEnum.Triple;
		public override int MaxPeople { get; set; } = 3;
		public override double PricePerNight { get; set; } = 200;
	}
}
