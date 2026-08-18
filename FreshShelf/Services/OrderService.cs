using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FreshShelf.Services;

public class OrderService
{

    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(ProductContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ReadOrderDto?> AddOrder(CreateOrderDto orderDto)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        if (userId == null) return null;

        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (restaurant == null) return null;

        Order order = _mapper.Map<Order>(orderDto);

        order.RestaurantId = restaurant.Id;

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadOrderDto>(order);
    }   

    public async Task<IEnumerable<ReadOrderDto>> GetOrders()
    {

        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) return Enumerable.Empty<ReadOrderDto>();

        if (user.IsInRole("Admin"))
        {
            var orders = await _context.Orders.ToListAsync();

            return _mapper.Map<IEnumerable<ReadOrderDto>>(orders);
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Enumerable.Empty<ReadOrderDto>();

        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (restaurant == null)
            return Enumerable.Empty<ReadOrderDto>();

        var restaurantOrders = await _context.Orders
            .Where(o => o.RestaurantId == restaurant.Id)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReadOrderDto>>(restaurantOrders);
    }

    public async Task<ReadOrderDto?> GetOrderById(int id)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null) return null;

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return null;

        if (user.IsInRole("Admin"))
            return _mapper.Map<ReadOrderDto>(order);

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null) return null;

        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (restaurant == null) return null;

        if (order.RestaurantId != restaurant.Id)
            return null;

        return _mapper.Map<ReadOrderDto>(order);
    }
    
    public async Task<ReadOrderDto?> UpdateDto(int id, UpdateOrderDto orderDto)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null) return null;

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
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

        _mapper.Map(orderDto, order);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadOrderDto>(order);
    }

    public async Task<ReadOrderDto?> PatchUpdateOrder(int id, JsonPatchDocument<UpdateOrderDto> patch)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null) return null;

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);

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

        var orderToUpdate = _mapper.Map<UpdateOrderDto>(order);
        patch.ApplyTo(orderToUpdate);
        _mapper.Map(orderToUpdate, order);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderDto>(order);
    }

    public async Task<ReadOrderDto?> DeleteOrder(int id)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null) return null;

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
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

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadOrderDto>(order);
    }
}
