using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshShelf.Services;

public class OrderItemService
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;

    public OrderItemService(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadOrderItemDto?> AddOrderItem(int orderId, CreateOrderItemDto orderItemDto)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null) return null;

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == orderItemDto.ProductId);
        if (product == null) return null;

        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ProductId = product.Id,
            Quantity = orderItemDto.Quantity,
            PriceAtPurchase = product.Price
        };

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }

    public async Task<IEnumerable<ReadOrderItemDto>> GetOrderItems()
    {
        var orderItem = await _context.OrderItems.ToListAsync();
        return _mapper.Map<IEnumerable<ReadOrderItemDto>>(orderItem);
    }

    public async Task<ReadOrderItemDto?> GetOrderItemById(int id)
    {
        var orderItem = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
        if (orderItem == null) return null;
        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }

    public async Task<IEnumerable<ReadOrderItemDto>> GetItemsOfOrders(int orderId)
    {
        var items = await _context.OrderItems
            .Where(items => items.OrderId == orderId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ReadOrderItemDto>>(items);
    }

    public async Task<ReadOrderItemDto?> UpdateOrderItem(int id, UpdateOrderItemDto orderItemDto)
    {
        var orderItem = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
        if(orderItem == null) return null;
        _mapper.Map(orderItemDto, orderItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }

    public async Task<ReadOrderItemDto?> DeleteOrderItem(int id)
    {
        var orderItem = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
        if (orderItem == null) return null;
        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }
}
