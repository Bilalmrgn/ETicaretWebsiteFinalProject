using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using System.Text;
using System.Threading.Tasks;

namespace Order.Persistence.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options) 
        {

        }

        //database tabloları
        public DbSet<Ordering> Orderings { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
