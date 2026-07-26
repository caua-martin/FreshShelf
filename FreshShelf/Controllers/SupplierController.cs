using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private ProductContext _context;
    private IMapper _mapper;

    public SupplierController(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateSupplier([FromBody] CreateSupplierDto supplierDto)
    {
        Supplier supplier = _mapper.Map<Supplier>(supplierDto);
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSupplierById),
            new { id = supplier.Id },
            supplier);
    }

    [HttpGet]
    public IEnumerable<ReadSupplierDto> GetSuppliers()
    {
        return _mapper.Map<List<ReadSupplierDto>>(_context.Suppliers.ToList());
    }

    [HttpGet("range")]
    public IEnumerable<ReadSupplierDto> GettingRangedSuppliers([FromQuery] int skip, [FromQuery] int take)
    {
        return _mapper.Map<List<ReadSupplierDto>>(_context.Suppliers.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetSupplierById(int id)
    {
        var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == id);
        if (supplier == null) return NotFound();
        var supplierDto = _mapper.Map<ReadSupplierDto>(supplier);
        return Ok(supplierDto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSupplier(int id)
    {
        var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
        if (supplier == null) return NotFound();
        _context.Remove(supplier);
        _context.SaveChanges();
        return NoContent();
    }
}
