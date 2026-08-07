using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FreshShelf.Services;

public class ProductService
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;

    public ProductService(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadProductDto?> AddProduct(CreateProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadProductDto>(product);
    }

    public async Task<IEnumerable<ReadProductDto>> GetProducts()
    {
        var product = await _context.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ReadProductDto>>(product);
    }

    public async Task<IEnumerable<ReadProductDto?>> GetProductsRange(int skip, int take)
    {
        var productsRanged = await _context.Products.Skip(skip).Take(take).ToListAsync();
        return _mapper.Map<IEnumerable<ReadProductDto>>(productsRanged);
    }

    public async Task<ReadProductDto?> GetProductsById(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
        if (product == null) return null;
        return _mapper.Map<ReadProductDto>(product);
    }

    public async Task<ReadProductDto?> UpdateProduct(int id, UpdateProductDto productDto)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
        if (product == null) return null;
        _mapper.Map(productDto, product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadProductDto>(product);
    }

    public async Task<ReadProductDto?> PatchUpdateProduct(int id, JsonPatchDocument<UpdateProductDto> patch)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return null;
        var productToUpdate = _mapper.Map<UpdateProductDto>(product);
        patch.ApplyTo(productToUpdate);

        _mapper.Map(productToUpdate, product);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadProductDto>(product);
    }

    public async Task<ReadProductDto?> DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
        if (product == null) return null;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadProductDto>(product);
    }
}
