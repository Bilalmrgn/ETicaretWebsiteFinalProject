using Cargo.Application.Repositories;
using Cargo.Domain;
using Cargo.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly CargoAppDbcontext _context;
        public ReadRepository(CargoAppDbcontext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        //Get all
        public IQueryable<T> GetAll() => Table;

        //GetById
        public Task<T> GetByIdAsync(string id) => Table.FirstOrDefaultAsync(data => data.Id == id);
    }
}
