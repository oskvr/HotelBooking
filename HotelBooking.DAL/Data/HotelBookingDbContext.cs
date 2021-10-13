using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Data
{

	public class HotelBookingDbContext : DbContext
	{
		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<BookingExtra> BookingExtras { get; set; }
		public DbSet<RoomType> RoomTypes { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HotelBookingDb;Trusted_Connection=True;");

			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.SeedHotels();
			modelBuilder.SeedRoomTypes();
			modelBuilder.SeedBookingExtras();
			base.OnModelCreating(modelBuilder);
		}
	}

	public static class Seeder
	{
		public static void SeedRoomTypes(this ModelBuilder modelBuilder)
		{
			_ = modelBuilder.Entity<RoomType>().HasData(
				new RoomType { Id = (int)RoomTypeIds.Single, Type = "Enkel", MaxCapacity = 1, PricePerNight = 800 },
				new RoomType { Id = (int)RoomTypeIds.Double, Type = "Dubbel", MaxCapacity = 2, PricePerNight = 1500 },
				new RoomType { Id = (int)RoomTypeIds.Triple, Type = "Trippel", MaxCapacity = 3, PricePerNight = 2000 }
				);
		}

		public static void SeedBookingExtras(this ModelBuilder builder)
		{
			int extrasId = 1;
			builder.Entity<BookingExtra>().HasData(
			new BookingExtra
			{
				Id = extrasId++,
				Type = "Transfer (tur/retur)",
				Cost = 500,
			},
			new BookingExtra
			{
				Id = extrasId++,
				Type = "All-Inclusive",
				Cost = 1500,
			},
			new BookingExtra
			{
				Id = extrasId++,
				Type = "Pool",
				Cost = 200,
			},
			new BookingExtra
			{
				Id = extrasId++,
				Type = "Frukost",
				Cost = 500,
			}
			); ;
		}
		public static void SeedHotels(this ModelBuilder modelBuilder)
		{
			int hotelId = 1;
			int roomId = 1;
			Random random = new();
			modelBuilder.Entity<Hotel>().HasData(
				new Hotel { Id = hotelId++, Image = new Uri("https://exp.cdn-hotels.com/hotels/7000000/6530000/6528700/6528699/845b25c6_z.jpg?impolicy=fcrop&w=1000&h=666&q=medium"), City = "Luleå", Name = "Clarion Hotel Sense", Rating = 4 },
				new Hotel { Id = hotelId++, Image = new Uri("https://exp.cdn-hotels.com/hotels/23000000/22470000/22469500/22469474/9de9b5a0_z.jpg?impolicy=fcrop&w=1000&h=666&q=medium"), City = "Sollefteå", Name = "Hotell Hallstaberget", Rating = 3 },
				new Hotel { Id = hotelId++, Image = new Uri("https://exp.cdn-hotels.com/hotels/1000000/910000/905200/905160/644c6fc5_z.jpg?impolicy=fcrop&w=1000&h=666&q=medium"), City = "Stockholm", Name = "Clarion Hotel Stockholm", Rating = 5 },
				new Hotel { Id = hotelId++, Image = new Uri("https://exp.cdn-hotels.com/hotels/20000000/19080000/19075200/19075129/fb2956d9_z.jpg?impolicy=fcrop&w=1000&h=666&q=medium"), City = "Uppsala", Name = "Arenahotellet i Uppsala", Rating = 3 },
				new Hotel { Id = hotelId++, Image = new Uri("https://exp.cdn-hotels.com/hotels/1000000/30000/24800/24747/eeb8edc0_z.jpg?impolicy=fcrop&w=1000&h=666&q=medium"), City = "Göteborg", Name = "Gothia Towers", Rating = 5 }
				);
			modelBuilder.Entity<Room>().HasData(
				new Room
				{
					Id = roomId++,
					HotelId = 1,
					RoomTypeId = (int)RoomTypeIds.Single
				},
				new Room
				{
					Id = roomId++,
					HotelId = 1,
					RoomTypeId = (int)RoomTypeIds.Double
				},
				new Room
				{
					Id = roomId++,
					HotelId = 1,
					RoomTypeId = (int)RoomTypeIds.Triple
				},
				new Room
				{
					Id = roomId++,
					HotelId = 2,
					RoomTypeId = (int)RoomTypeIds.Single
				},
				new Room
				{
					Id = roomId++,
					HotelId = 2,
					RoomTypeId = (int)RoomTypeIds.Double
				},
				new Room
				{
					Id = roomId++,
					HotelId = 2,
					RoomTypeId = (int)RoomTypeIds.Triple
				},
				new Room
				{
					Id = roomId++,
					HotelId = 3,
					RoomTypeId = (int)RoomTypeIds.Single
				},
				new Room
				{
					Id = roomId++,
					HotelId = 3,
					RoomTypeId = (int)RoomTypeIds.Double
				},
				new Room
				{
					Id = roomId++,
					HotelId = 3,
					RoomTypeId = (int)RoomTypeIds.Triple
				},
				new Room
				{
					Id = roomId++,
					HotelId = 4,
					RoomTypeId = (int)RoomTypeIds.Single
				},
				new Room
				{
					Id = roomId++,
					HotelId = 4,
					RoomTypeId = (int)RoomTypeIds.Double
				},
				new Room
				{
					Id = roomId++,
					HotelId = 4,
					RoomTypeId = (int)RoomTypeIds.Triple
				},
				new Room
				{
					Id = roomId++,
					HotelId = 5,
					RoomTypeId = (int)RoomTypeIds.Single
				},
				new Room
				{
					Id = roomId++,
					HotelId = 5,
					RoomTypeId = (int)RoomTypeIds.Double
				},
				new Room
				{
					Id = roomId++,
					HotelId = 5,
					RoomTypeId = (int)RoomTypeIds.Triple
				}
			);
		}
	}
}
