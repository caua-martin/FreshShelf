using FreshShelf.Data;
using FreshShelf.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private ProductContext _context;

    public ProductController(ProductContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProductById),
            new { id = product.Id},
            product);
    }

    [HttpGet]
    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products;
    }

    [HttpGet("range")]
    public IEnumerable<Product> GettingRangeProducts([FromQuery] int skip, [FromQuery] int take)
    {
        return _context.Products.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _context.Products.FirstOrDefault(product => product.Id == id);
        if(product == null) return NotFound();
        return Ok(product);
    }
}
