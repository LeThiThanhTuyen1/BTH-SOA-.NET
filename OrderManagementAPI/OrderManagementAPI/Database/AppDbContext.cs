using OrderManagementAPI.Database;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Model;

namespace OrderManagementAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("order_items");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomerName).HasColumnName("customer_name");
                entity.Property(e => e.CustomerEmail).HasColumnName("customer_email");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            // Cấu hình cho bảng "order_items"
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductName).HasColumnName("product_name");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
                entity.Property(e => e.TotalPrice).HasColumnName("total_price");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });
        }
    }
}
