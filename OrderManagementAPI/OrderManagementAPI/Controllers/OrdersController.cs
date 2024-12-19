using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Model;
using OrderManagementAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace OrderManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = _context.Orders
                         .Include(o => o.OrderItems) 
                         .ToList();

        return Ok(orders);
    }

    // GET: api/orders/{id}
    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        // Tải trước OrderItems cho đơn hàng cụ thể
        var order = _context.Orders
                            .Include(o => o.OrderItems) // Tải trước OrderItems
                            .FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // POST: api/orders
    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        order.CreatedAt = DateTime.Now;
        order.UpdatedAt = DateTime.Now;

        // Gán đối tượng Order cho mỗi OrderItem
        foreach (var item in order.OrderItems)
        {
            item.Order = order; // Gán Order cho từng OrderItem từ server
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;
        }

        _context.Orders.Add(order);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    // PUT: api/orders/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] Order order)
    {
        var existingOrder = _context.Orders.Find(id);
        if (existingOrder == null)
        {
            return NotFound();
        }

        existingOrder.CustomerName = order.CustomerName;
        existingOrder.CustomerEmail = order.CustomerEmail;
        existingOrder.Status = order.Status;
        existingOrder.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/orders/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        _context.SaveChanges();
        return NoContent();
    }
}
