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
		public DbSet<Rating> Ratings { get; set; }
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
				new RoomType { Id = (int)RoomTypeIds.Single, Type = "Single Room", MaxCapacity = 1, PricePerNight = 100 },
				new RoomType { Id = (int)RoomTypeIds.Double, Type = "Double Room", MaxCapacity = 2, PricePerNight = 150 },
				new RoomType { Id = (int)RoomTypeIds.Triple, Type = "Triple Room", MaxCapacity = 3, PricePerNight = 200 }
				);
		}

		public static void SeedBookingExtras(this ModelBuilder builder)
		{
			int extrasId = 1;
			builder.Entity<BookingExtra>().HasData(
			new BookingExtra
			{
				Id = extrasId++,
				Type = "Transport",
				Cost = 20,
			},
			new BookingExtra
			{
				Id = extrasId++,
				Type = "All-Inclusive",
				Cost = 100,
			},
			new BookingExtra
			{
				Id = extrasId++,
				Type = "Pool",
				Cost = 30,
			},
			new BookingExtra
			{
				Id = extrasId++,
				Type = "Breakfast",
				Cost = 50,
			}
			);
		}
		public static void SeedHotels(this ModelBuilder modelBuilder)
		{
			int hotelId = 1;
			int roomId = 1;
			int ratingId = 1;
			Random random = new();
			modelBuilder.Entity<Hotel>().HasData(
				new Hotel { Id = hotelId++, ThumbnailImage = new Uri("https://picsum.photos/500"), Country = "Sweden", Name = "Plaza Bay Hotel" },
				new Hotel { Id = hotelId++, ThumbnailImage = new Uri("https://picsum.photos/500"), Country = "Sweden", Name = "Stadshotellet" },
				new Hotel { Id = hotelId++, ThumbnailImage = new Uri("https://picsum.photos/500"), Country = "Sweden", Name = "Sunkstället" },
				new Hotel { Id = hotelId++, ThumbnailImage = new Uri("https://picsum.photos/500"), Country = "Sweden", Name = "Stugstugan" }
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
				}
			);
			modelBuilder.Entity<Rating>().HasData(
			new Rating { Id = ratingId++, HotelId = 1, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 1, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 1, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 1, Score = random.Next(0, 6) },

			new Rating { Id = ratingId++, HotelId = 2, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 2, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 2, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 2, Score = random.Next(0, 6) },

			new Rating { Id = ratingId++, HotelId = 3, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 3, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 3, Score = random.Next(0, 6) },
			new Rating { Id = ratingId++, HotelId = 3, Score = random.Next(0, 6) }
			);
		}
	}
}
