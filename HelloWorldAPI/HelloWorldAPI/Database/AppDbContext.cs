using HelloWorldAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.IdUser); 
        }
    }
}
