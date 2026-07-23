using AutoMapper;
using Azure;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
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
     
    private ProductContext _context;
    private IMapper _mapper;

    public ProductController(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Add a product in Database
    /// </summary>
    /// <param name="productDto">a</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddProduct([FromBody] CreateProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProductById),
            new { id = product.Id},
            product);
    }

    [HttpGet]
    // IActionResult?
    public IEnumerable<ReadProductDto> GetProducts()
    {
        return _mapper.Map<List<ReadProductDto>>(_context.Products.ToList());
    }

    [HttpGet("range")]
    // IActionResult?
    public IEnumerable<ReadProductDto> GettingRangeProducts([FromQuery] int skip, [FromQuery] int take)
    {
        return _mapper.Map<List<ReadProductDto>>(_context.Products.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _context.Products.FirstOrDefault(product => product.Id == id);
        if(product == null) return NotFound();
        var productDto = _mapper.Map<ReadProductDto>(product);
        return Ok(productDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
    {
        var product = _context.Products.FirstOrDefault(product => product.Id == id);
        if (product == null) return NotFound();
        _mapper.Map(productDto, product);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PatchUpdateProduct(
    int id,
    [FromBody] JsonPatchDocument<UpdateProductDto> patch)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        var productToUpdate = _mapper.Map<UpdateProductDto>(product);

        patch.ApplyTo(productToUpdate, ModelState);

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        if (!TryValidateModel(productToUpdate))
            return ValidationProblem(ModelState);

        _mapper.Map(productToUpdate, product);

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();
        _context.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}