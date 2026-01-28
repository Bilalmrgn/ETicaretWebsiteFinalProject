using Discount.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Discount.Context
{
    public class DapperContext : DbContext
    {
        //dapper configuration
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");//DefaultConnection = appsettings deki default connection key im

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BILALMERGEN\\SQLEXPRESS;initial Catalog=ECommerceFinalProjectDiscountDB;integrated Security=true");

        }
        public DbSet<Coupon> Coupons { get; set; }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
