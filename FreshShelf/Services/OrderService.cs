using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshShelf.Services;

public class OrderService
{

    private readonly ProductContext _context;
    private readonly IMapper _mapper;

    public OrderService(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadOrderDto> AddOrder(CreateOrderDto orderDto)
    {
        Order order = _mapper.Map<Order>(orderDto);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderDto>(order);
    }   

    public async Task<IEnumerable<ReadOrderDto>> GetOrders()
    {
        var order = await _context.Orders.ToListAsync();
        return _mapper.Map<IEnumerable<ReadOrderDto>>(order);
    }

    public async Task<ReadOrderDto?> GetOrderById(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == id);
        if (order == null) return null;
        return _mapper.Map<ReadOrderDto>(order);
    }
    
    public async Task<ReadOrderDto?> UpdateDto(int id, UpdateOrderDto orderDto)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == id);
        if (order == null) return null;
        _mapper.Map(orderDto, order);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderDto>(order);
    }

    public async Task<ReadOrderDto?> PatchUpdateOrder(int id, JsonPatchDocument<UpdateOrderDto> patch)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == id);
        if (order == null) return null;
        var orderToUpdate = _mapper.Map<UpdateOrderDto>(order);

        patch.ApplyTo(orderToUpdate);

        _mapper.Map(orderToUpdate, order);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadOrderDto>(order);
    }

    public async Task<ReadOrderDto?> DeleteOrder(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == id);
        if (order == null) return null;
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderDto>(order);
    }
}
