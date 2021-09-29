using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
    //public record RegistrationResult(User user);
    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string firstName, string lastName, string password, string confirmPassword);
        Task<LoginResult> Login(string email, string password);
    }
}
