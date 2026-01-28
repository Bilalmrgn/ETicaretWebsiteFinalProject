using Domain;
using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Order.Persistence.Concrete
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly OrderDbContext _context;
        public OrderingRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Ordering ordering)
        {
            _context.AddAsync(ordering);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orderings.FindAsync(id);

            if (order != null)
            {
                return;
            }

            _context.Orderings.Remove(order);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Ordering>> GetAllAsync()
        {
            return await _context.Orderings.ToListAsync();
        }

        public async Task<Ordering> GetByIdAsync(int id)
        {
            return await _context.Orderings.FindAsync(id);
        }

        public async Task UpdateAsync(Ordering ordering)
        {
            _context.Orderings.Update(ordering);
            await _context.SaveChangesAsync();
        }
    }
}
