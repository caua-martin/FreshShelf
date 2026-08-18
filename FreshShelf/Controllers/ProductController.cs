using FreshShelf.Data.Dtos;
using FreshShelf.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Adds a product to the Database.
    /// </summary>
    /// <param name="productDto">The product data to be added</param>
    /// <returns>The newly created product</returns>
    /// <response code="201">The product was successfully created</response>
    [HttpPost]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDto productDto)
    {
        var product = await _productService.AddProduct(productDto);
        return CreatedAtAction(nameof(GetProductById),
            new { id = product.Id},
            product);
    }

    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <returns>A list of all products</returns>
    /// <response code="200">The products were succesffully retrieved</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }

    /// <summary>
    /// Gets a range of products.
    /// </summary>
    /// <param name="skip">The number of products that you want to skip</param>
    /// <param name="take">The number of products that you want to retrieve</param>
    /// <returns>A list of all taken products</returns>
    /// <response code="200">The ranged products were successffully retrieved</response>
    [HttpGet("range")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsRange([FromQuery] int skip, [FromQuery] int take)
    {
        var product = await _productService.GetProductsRange(skip, take);
        return Ok(product);
    }

    /// <summary>
    /// Gets a product by id.
    /// </summary>
    /// <param name="id">The id of the request product</param>
    /// <returns>The chosen product</returns>
    /// <response code="200">The product was succesffully retrieved</response>
    /// <response code="404">The product was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductsById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Updates a product by id.
    /// </summary>
    /// <param name="id">The id of the product to update</param>
    /// <param name="productDto">The product data to be updated</param>
    /// <returns>No content</returns>
    /// <response code="204">The product was successffully updated</response>
    /// <response code="404">The product was not found</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
    {
        var product = await _productService.UpdateProduct(id, productDto);
        if (product == null) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Partially updates a product.
    /// </summary>
    /// <param name="id">The id of the product to update</param>
    /// <param name="patch">The JSON patch document containing the changes to apply</param>
    /// <returns>No content</returns>
    /// <response code="204">The product was successfully updated</response>
    /// <response code="404">The product was not found</response>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchUpdateProduct(
    int id,
    [FromBody] JsonPatchDocument<UpdateProductDto> patch)
    {
        var productDto = await _productService.PatchUpdateProduct(id, patch);
        if (productDto == null) return NotFound();
        //if (!ModelState.IsValid)
        //    return ValidationProblem(ModelState);

        //if (!TryValidateModel(productDto))
        //    return ValidationProblem(ModelState);
        return NoContent();
    }

    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <param name="id">The id of the product to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">The product was successfully deleted</response>
    /// <response code="404">The product was not found</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productService.DeleteProduct(id);
        if (product == null) return NotFound();
        return NoContent();
    }
}