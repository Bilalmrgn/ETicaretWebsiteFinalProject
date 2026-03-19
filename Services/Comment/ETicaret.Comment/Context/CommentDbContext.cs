using ETicaret.Comment.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Comment.Context
{
    public class CommentDbContext : DbContext
    {
        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

        public DbSet<UserComment> UserComments { get; set; }
    }
}
