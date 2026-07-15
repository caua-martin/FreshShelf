using FreshShelf.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private static List<Supplier> suppliers = new List<Supplier>();
    private static int nextId = 0;

    [HttpPost]
    public CreatedAtActionResult CreateSupplier([FromBody] Supplier supplier)
    {
        supplier.Id = nextId++;
        suppliers.Add(supplier);
        return CreatedAtAction(nameof(GetSupplierById),
            new { id = supplier.Id },
            supplier);
    }

    [HttpGet]
    public IEnumerable<Supplier> GetAllSuppliers()
    {
        return suppliers;
    }

    [HttpGet("range")]
    public IEnumerable<Supplier> GettingRangedSuppliers([FromQuery] int skip, [FromQuery] int take)
    {
        return suppliers.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetSupplierById(int id)
    {
        var supplier = suppliers.FirstOrDefault(x => x.Id == id);
        if (supplier == null) return NotFound();
        return Ok(supplier);
    }
}
