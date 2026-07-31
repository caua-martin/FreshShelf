using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using FreshShelf.Services;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private ProductContext _context;
    private IMapper _mapper;
    private SupplierService _supplierService;

    public SupplierController(ProductContext context, IMapper mapper, SupplierService supplierService)
    {
        _context = context;
        _mapper = mapper;
        _supplierService = supplierService;
    }

    [HttpPost]
    public async Task<IActionResult> AddSupplier([FromBody] CreateSupplierDto supplierDto)
    {
        var supplier = await _supplierService.AddSupplier(supplierDto);
        return CreatedAtAction(nameof(GetSupplierById),
            new { id = supplier.Id },
            supplier);
    }

    [HttpGet]
    public async Task<IActionResult> GetSuppliers()
    {
        var supplier = await _supplierService.GetSuppliers();

        return Ok(supplier);
    }

    [HttpGet("range")]
    public async Task<IActionResult> GettingRangedSuppliers([FromQuery] int skip, [FromQuery] int take)
    {
        var suppliersRanged = await _supplierService.GettingRangedSuppliers(skip, take);

        return Ok(suppliersRanged);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupplierById(int id)
    {
        var supplier = await _supplierService.GetSupplierById(id);
        if(supplier == null) return NotFound();
        return Ok(supplier);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(int id, UpdateSupplierDto supplierDto)
    {
        var supplier = await _supplierService.UpdateSupplier(id, supplierDto);
        if(supplier == null) return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUpdateSupplier(int id, JsonPatchDocument<UpdateSupplierDto> patch)
    {
        var supplier = await _supplierService.PatchUpdateSupplier(id, patch);
        if(supplier == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await _supplierService.DeleteSupplier(id);
        if(supplier == null) return NotFound();
        return NoContent();
    }
}
