using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Database;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;
        _context.Products.Add(product);
        _context.SaveChanges();
        return Ok(product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        var existingProduct = _context.Products.Find(id);
        if (existingProduct == null) return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
        return Ok(existingProduct);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return NotFound();

        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}
