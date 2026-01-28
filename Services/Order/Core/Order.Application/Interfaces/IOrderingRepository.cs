using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Interfaces
{
    public interface IOrderingRepository
    {
        Task<List<Ordering>> GetAllAsync();
        Task<Ordering> GetByIdAsync(int id);
        Task CreateAsync(Ordering ordering);
        Task UpdateAsync(Ordering ordering);
        Task DeleteAsync(int id);
    }
}
