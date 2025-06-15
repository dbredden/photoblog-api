
using Microsoft.EntityFrameworkCore;
using PhotoBlog.Domain.Entities;

namespace PhotoBlog.Database
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) { }

        public DbSet<PostEntity> Posts { get; set; }
    }
}
