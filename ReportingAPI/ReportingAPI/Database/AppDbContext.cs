using Microsoft.EntityFrameworkCore;
using ReportingAPI.Models;

namespace ReportingAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<OrdersReport> OrdersReports { get; set; }
        public DbSet<ProductReport> ProductReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ánh xạ bảng orders_reports
            modelBuilder.Entity<OrdersReport>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.TotalRevenue).HasColumnName("total_revenue");
                entity.Property(e => e.TotalCost).HasColumnName("total_cost");
                entity.Property(e => e.TotalProfit).HasColumnName("total_profit");
            });

            modelBuilder.Entity<ProductReport>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.TotalSold).HasColumnName("total_sold");
                entity.Property(e => e.OrderReportId).HasColumnName("order_report_id");
                entity.Property(e => e.Cost).HasColumnName("cost");
                entity.Property(e => e.Revenue).HasColumnName("revenue");
                entity.Property(e => e.Profit).HasColumnName("profit");
            });
        }
    }
}
