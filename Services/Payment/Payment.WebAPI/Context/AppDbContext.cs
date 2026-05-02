using Microsoft.EntityFrameworkCore;
using Payment.WebAPI.Models;
using System.Collections.Generic;

namespace Payment.WebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Paymenta> Paymentas { get; set; }
    }
}
