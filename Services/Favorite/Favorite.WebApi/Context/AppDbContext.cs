using Microsoft.EntityFrameworkCore;
using Favorite.WebApi.Model;
namespace Favorite.WebApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FavoriteModel> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteModel>(entity =>
            {
                entity.ToTable("Favorites");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserId)
                      .IsRequired();

                entity.Property(x => x.ProductId)
                      .IsRequired();

                entity.Property(x => x.CreatedAt)
                      .IsRequired();

                entity.HasIndex(x => new { x.UserId, x.ProductId })
                      .IsUnique();
            });
        }
    }
}
