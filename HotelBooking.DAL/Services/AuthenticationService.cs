using HotelBooking.DAL.Data;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Shared;
using Microsoft.AspNet.Identity;
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

		public AuthenticationService(HotelBookingDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<LoginResult> Login(string email, string password)
		{

			var hasher = new PasswordHasher();
			User user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
			bool passwordMatches = hasher.VerifyHashedPassword(user?.Password, password) == PasswordVerificationResult.Success;
			if (user is not null && passwordMatches)
			{
				return new LoginResult(User: user, IsSuccess: true);
			}
			else
			{
				return new LoginResult(User: user, IsSuccess: false);
			}
		}

		public async Task<RegistrationResult> Register(string email, string firstName, string lastName, string password, string confirmPassword)
		{
			if (password != confirmPassword)
			{
				return new RegistrationResult(User: null, IsSuccess: false);
			}
			IPasswordHasher hasher = new PasswordHasher();
			string hashedPassword = hasher.HashPassword(password);
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
