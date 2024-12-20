using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
