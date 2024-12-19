using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingAPI.Models
{
    [Table("orders_reports")]
    public class OrdersReport
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
