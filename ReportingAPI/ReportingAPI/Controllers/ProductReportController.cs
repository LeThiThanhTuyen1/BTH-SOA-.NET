using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportingAPI.Database;
using ReportingAPI.Models;

namespace ReportingAPI.Controllers
{
    [Authorize]
    [Route("api/reports/products")]
    [ApiController]
    public class ProductReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProductReports()
        {
            var reports = _context.ProductReports.ToList();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductReportById(int id)
        {
            var report = _context.ProductReports.FirstOrDefault(p => p.Id == id);
            if (report == null)
                return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public IActionResult CreateProductReport([FromBody] ProductReport report)
        {
            if (report == null)
                return BadRequest();

            report.Profit = report.Revenue - report.Cost;
            _context.ProductReports.Add(report);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductReportById), new { id = report.Id }, report);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductReport(int id)
        {
            var report = _context.ProductReports.FirstOrDefault(p => p.Id == id);
            if (report == null)
                return NotFound();

            _context.ProductReports.Remove(report);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
