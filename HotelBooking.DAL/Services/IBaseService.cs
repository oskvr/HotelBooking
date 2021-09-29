using HotelBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DAL.Services
{
	public interface IBaseService<T> where T : BaseEntity
	{
		Task<T> GetById(int id);
		Task<IEnumerable<T>> GetAll();
		Task Add(T entity);
		Task Edit(T entity);
		Task Delete(T entity);
	}
}
