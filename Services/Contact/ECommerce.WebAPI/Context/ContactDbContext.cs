using ECommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.WebAPI.Context
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
