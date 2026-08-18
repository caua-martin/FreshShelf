using FreshShelf.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using FreshShelf.Services;
using Microsoft.AspNetCore.Authorization;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    private readonly SupplierService _supplierService;

    public SupplierController(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    /// <summary>
    /// Adds a supplier to the Database.
    /// </summary>
    /// <param name="supplierDto">The supplier data to be added</param>
    /// <returns>The newly created supplier</returns>
    /// <response code="201">The supplier was successfully created</response>
    [HttpPost]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddSupplier([FromBody] CreateSupplierDto supplierDto)
    {
        var supplier = await _supplierService.AddSupplier(supplierDto);
        return CreatedAtAction(nameof(GetSupplierById),
            new { id = supplier.Id },
            supplier);
    }

    /// <summary>
    /// Gets all suppliers.
    /// </summary>
    /// <returns>A list of all suppliers</returns>
    /// <response code="200">The suppliers were succesffully retrieved</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSuppliers()
    {
        var suppliers = await _supplierService.GetSuppliers();

        return Ok(suppliers);
    }

    /// <summary>
    /// Gets a range of suppliers.
    /// </summary>
    /// <param name="skip">The number of suppliers that you want to skip</param>
    /// <param name="take">The number of suppliers that you want to retrieve</param>
    /// <returns>A list of all taken suppliers</returns>
    /// <response code="200">The ranged suppliers were successffully retrieved</response>
    [HttpGet("range")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSuppliersRange([FromQuery] int skip, [FromQuery] int take)
    {
        var suppliersRanged = await _supplierService.GetSuppliersRange(skip, take);

        return Ok(suppliersRanged);
    }

    /// <summary>
    /// Gets a supplier by id.
    /// </summary>
    /// <param name="id">The id of the requested supplier</param>
    /// <returns>The chosen supplier</returns>
    /// <response code="200">The supplier was succesffully retrieved</response>
    /// <response code="404">The supplier was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSupplierById(int id)
    {
        var supplier = await _supplierService.GetSupplierById(id);
        if(supplier == null) return NotFound();
        return Ok(supplier);
    }

    /// <summary>
    /// Updates a supplier by id.
    /// </summary>
    /// <param name="id">The id of the supplier to update</param>
    /// <param name="supplierDto">The supplier data to be updated</param>
    /// <returns>No content</returns>
    /// <response code="204">The supplier was successffully updated</response>
    /// <response code="404">The supplier was not found</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierDto supplierDto)
    {
        var supplier = await _supplierService.UpdateSupplier(id, supplierDto);
        if(supplier == null) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Partially updates a supplier.
    /// </summary>
    /// <param name="id">The id of the supplier to update</param>
    /// <param name="patch">The JSON patch document containing the changes to apply</param>
    /// <returns>No content</returns>
    /// <response code="204">The supplier was successfully updated</response>
    /// <response code="404">The supplier was not found</response>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchUpdateSupplier(int id, JsonPatchDocument<UpdateSupplierDto> patch)
    {
        var supplier = await _supplierService.PatchUpdateSupplier(id, patch);
        if(supplier == null) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Deletes a supplier.
    /// </summary>
    /// <param name="id">The id of the supplier to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">The supplier was successfully deleted</response>
    /// <response code="404">The supplier was not found</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await _supplierService.DeleteSupplier(id);
        if(supplier == null) return NotFound();
        return NoContent();
    }
}
