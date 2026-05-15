using Invoice.WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Invoice.WebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<InvoiceModel> Invoices { get; set; }
    }
}
