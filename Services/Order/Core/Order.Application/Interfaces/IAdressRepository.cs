using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Interfaces
{
    public interface IAdressRepository
    {
        Task<List<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(int id);
        Task CreateAsync(Address address);
        Task UpdateAsync(Address address);
        Task DeleteAsync(Address address);
    }
}
