using AutoMapper;
using Azure;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using FreshShelf.Services;
using Microsoft.AspNetCore.JsonPatch;
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
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Add a product in Database
    /// </summary>
    /// <param name="productDto">a</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDto productDto)
    {
        var product = await _productService.AddProduct(productDto);
        return CreatedAtAction(nameof(GetProductById),
            new { id = product.Id},
            product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var product = await _productService.GetProducts();
        return Ok(product);
    }

    [HttpGet("range")]
    public async Task<IActionResult> GetProductsRange([FromQuery] int skip, [FromQuery] int take)
    {
        var product = await _productService.GetProductsRange(skip, take);
        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductsById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
    {
        var product = await _productService.UpdateProduct(id, productDto);
        if (product == null) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productService.DeleteProduct(id);
        if (product == null) return NotFound();
        return NoContent();
    }
}