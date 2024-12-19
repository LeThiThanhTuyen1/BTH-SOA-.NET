using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportingAPI.Database;
using ReportingAPI.Models;

namespace ReportingAPI.Controllers
{
    [Authorize]
    [Route("api/reports/orders")]
    [ApiController]
    public class OrdersReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllOrdersReports()
        {
            var reports = _context.OrdersReports.ToList();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderReportById(int id)
        {
            var report = _context.OrdersReports.FirstOrDefault(o => o.Id == id);
            if (report == null)
                return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public IActionResult CreateOrderReport([FromBody] OrdersReport report)
        {
            if (report == null)
                return BadRequest();

            report.TotalProfit = report.TotalRevenue - report.TotalCost;
            _context.OrdersReports.Add(report);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetOrderReportById), new { id = report.Id }, report);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderReport(int id)
        {
            var report = _context.OrdersReports.FirstOrDefault(o => o.Id == id);
            if (report == null)
                return NotFound();

            _context.OrdersReports.Remove(report);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
