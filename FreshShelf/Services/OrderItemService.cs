using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FreshShelf.Services;

public class OrderItemService
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderItemService(ProductContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ReadOrderItemDto?> AddOrderItem(int orderId, CreateOrderItemDto orderItemDto)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) return null;

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null) return null;

        if (!user.IsInRole("Admin"))
        {
            var userId = user
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (userId == null) return null;

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (restaurant == null) return null;

            if (order.RestaurantId != restaurant.Id)
                return null;
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == orderItemDto.ProductId);

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
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return Enumerable.Empty<ReadOrderItemDto>();

        if (user.IsInRole("Admin"))
        {
            var orderItems = await _context.OrderItems
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReadOrderItemDto>>(orderItems);
        }

        var userId = user
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        if (userId == null)
            return Enumerable.Empty<ReadOrderItemDto>();

        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (restaurant == null)
            return Enumerable.Empty<ReadOrderItemDto>();

        var orderItemsRestaurant = await _context.OrderItems
            .Where(oi => oi.Order.RestaurantId == restaurant.Id)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReadOrderItemDto>>(orderItemsRestaurant);
    }

    public async Task<ReadOrderItemDto?> GetOrderItemById(int id)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) return null;

        var orderItem = await _context.OrderItems
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == id);

        if (orderItem == null) return null;

        if (!user.IsInRole("Admin"))
        {
            var userId = user
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (userId == null) return null;

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (restaurant == null) return null;

            if (orderItem.Order.RestaurantId != restaurant.Id)
                return null;
        }

        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }

    public async Task<IEnumerable<ReadOrderItemDto>> GetItemsOfOrders(int orderId)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return Enumerable.Empty<ReadOrderItemDto>();

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return Enumerable.Empty<ReadOrderItemDto>();

        if (!user.IsInRole("Admin"))
        {
            var userId = user
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (userId == null)
                return Enumerable.Empty<ReadOrderItemDto>();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (restaurant == null)
                return Enumerable.Empty<ReadOrderItemDto>();

            if (order.RestaurantId != restaurant.Id)
                return Enumerable.Empty<ReadOrderItemDto>();
        }

        var items = await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReadOrderItemDto>>(items);
    }

    public async Task<ReadOrderItemDto?> UpdateOrderItem(int id, UpdateOrderItemDto orderItemDto)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) return null;

        var orderItem = await _context.OrderItems
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == id);

        if (orderItem == null) return null;

        if (!user.IsInRole("Admin"))
        {
            var userId = user
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (userId == null) return null;

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (restaurant == null) return null;

            if (orderItem.Order.RestaurantId != restaurant.Id)
                return null;
        }

        _mapper.Map(orderItemDto, orderItem);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }

    public async Task<ReadOrderItemDto?> DeleteOrderItem(int id)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) return null;

        var orderItem = await _context.OrderItems
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == id);

        if (orderItem == null) return null;

        if (!user.IsInRole("Admin"))
        {
            var userId = user
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (userId == null) return null;

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (restaurant == null) return null;

            if (orderItem.Order.RestaurantId != restaurant.Id)
                return null;
        }

        _context.OrderItems.Remove(orderItem);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadOrderItemDto>(orderItem);
    }
}
