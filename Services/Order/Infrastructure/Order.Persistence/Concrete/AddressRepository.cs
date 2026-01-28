using Domain;
using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Persistence.Concrete
{
    public class AddressRepository : IAdressRepository
    {
        private readonly OrderDbContext _context;
        public AddressRepository(OrderDbContext context)
        {
            _context = context;
        }

        //create address
        public async Task CreateAsync(Address address)
        {
            _context.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            
            if(address is null)
            {
                return;
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }
    }
}
