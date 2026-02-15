using Cargo.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Persistence.Context
{
    public class CargoAppDbcontext : DbContext
    {
        public CargoAppDbcontext(DbContextOptions<CargoAppDbcontext> options) : base(options) { }
        public DbSet<CargoCompany> CargoCompanies { get; set; }
        public DbSet<CargoCustomer> CargoCustomers { get; set; }
        public DbSet<CargoDetail> CargoDetails { get; set; }
        public DbSet<CargoOperation> CargoOperations { get; set; }
    }
}
