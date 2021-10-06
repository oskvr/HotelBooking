using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public record LoginResult(User User, bool IsSuccess);
	public record RegistrationResult(User User, bool IsSuccess);
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HotelBookingDbContext dbContext;
		private readonly GlobalStore store;

		public AuthenticationService(HotelBookingDbContext dbContext, GlobalStore store)
		{
			this.dbContext = dbContext;
			this.store = store;
		}

		public async Task<LoginResult> Login(string email, string password)
		{

			PasswordHasher<User> hasher = new();
			User user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
			if (user is not null)
			{
				bool passwordMatches = hasher.VerifyHashedPassword(user, user?.Password, password) == PasswordVerificationResult.Success;
				if (passwordMatches)
				{
					store.CurrentUser = user;
					return new LoginResult(User: user, IsSuccess: true);
				}
			}
			return new LoginResult(User: user, IsSuccess: false);
		}

		private async Task<bool> UserExists(string email)
		{
			return await dbContext.Users.AnyAsync(user => user.Email == email);
		}
		public async Task<RegistrationResult> Register(string email, string firstName, string lastName, string password, string confirmPassword)
		{
			if (password != confirmPassword)
			{
				return new RegistrationResult(User: null, IsSuccess: false);
			}
			var userExists = await UserExists(email);
			if (userExists)
			{
				return new RegistrationResult(User: null, IsSuccess: false);
			}
			PasswordHasher<User> hasher = new();
			string hashedPassword = hasher.HashPassword(null, password);
			User newUser = new()
			{
				Email = email,
				FirstName = firstName,
				LastName = lastName,
				Password = hashedPassword
			};
			dbContext.Users.Add(newUser);
			await dbContext.SaveChangesAsync();
			return new RegistrationResult(User: newUser, IsSuccess: true);

		}
	}
}
