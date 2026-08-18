using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FreshShelf.Services;

public class SupplierService
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SupplierService(ProductContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ReadSupplierDto> AddSupplier(CreateSupplierDto supplierDto)
    {
        Supplier supplier = _mapper.Map<Supplier>(supplierDto);

        var userId = _httpContextAccessor.HttpContext?
        .User
        .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            throw new UnauthorizedAccessException();

        supplier.UserId = userId;

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReadSupplierDto>(supplier);
    }

    public async Task<IEnumerable<ReadSupplierDto>> GetSuppliers()
    {
        var suppliers = await _context.Suppliers.ToListAsync();

        return _mapper.Map<IEnumerable<ReadSupplierDto>>(suppliers);
    }

    public async Task<IEnumerable<ReadSupplierDto>> GetSuppliersRange(int skip, int take)
    {
        var suppliersRanged = await _context.Suppliers.Skip(skip).Take(take).ToListAsync();

        return _mapper.Map<IEnumerable<ReadSupplierDto>>(suppliersRanged);
    }

    public async Task<ReadSupplierDto?> GetSupplierById(int id)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == id);
        if(supplier == null) return null;
        return _mapper.Map<ReadSupplierDto>(supplier);
    }

    public async Task<ReadSupplierDto?> UpdateSupplier(int id, UpdateSupplierDto supplierDto)
    {
        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(supplier => supplier.Id == id);

        if (supplier == null)
            return null;

        var userId = _httpContextAccessor.HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            throw new UnauthorizedAccessException();

        var isAdmin = _httpContextAccessor.HttpContext?
            .User
            .IsInRole("Admin") ?? false;

        if (!isAdmin && supplier.UserId != userId)
            return null;

        _mapper.Map(supplierDto, supplier);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadSupplierDto>(supplier);
    }

    public async Task<ReadSupplierDto?> PatchUpdateSupplier(int id, JsonPatchDocument<UpdateSupplierDto> patch)
    {
        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(supplier => supplier.Id == id);

        if (supplier == null) return null;

        var userId = _httpContextAccessor.HttpContext?
        .User
        .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            throw new UnauthorizedAccessException();

        var isAdmin = _httpContextAccessor.HttpContext?
            .User
            .IsInRole("Admin") ?? false;

        if (!isAdmin && supplier.UserId != userId)
            return null;

        var supplierToUpdate = _mapper.Map<UpdateSupplierDto>(supplier);

        patch.ApplyTo(supplierToUpdate);

        _mapper.Map(supplierToUpdate, supplier);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadSupplierDto>(supplier);
    }

    public async Task<ReadSupplierDto?> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == id);
        if(supplier == null) return null;

        var userId = _httpContextAccessor.HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            throw new UnauthorizedAccessException();

        var isAdmin = _httpContextAccessor.HttpContext?
            .User
            .IsInRole("Admin") ?? false;

        if (!isAdmin && supplier.UserId != userId)
            return null;

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReadSupplierDto>(supplier);
    }
}