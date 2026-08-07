using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace FreshShelf.Services;

public class RestaurantService
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;

    public RestaurantService(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadRestaurantDto> AddRestaurant(CreateRestaurantDto restaurantDto)
    {
        Restaurant restaurant = _mapper.Map<Restaurant>(restaurantDto);
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadRestaurantDto>(restaurant);
    }

    public async Task<IEnumerable<ReadRestaurantDto>> GetRestaurants()
    {
        var restaurants = await _context.Restaurants.ToListAsync();
        return _mapper.Map<IEnumerable<ReadRestaurantDto>>(restaurants);
    }

    public async Task<IEnumerable<ReadRestaurantDto>> GetRestaurantsRange(int skip, int take)
    {
        var restaurantsRanged = await _context.Restaurants.Skip(skip).Take(take).ToListAsync();

        return _mapper.Map<IEnumerable<ReadRestaurantDto>>(restaurantsRanged);
    }

    public async Task<ReadRestaurantDto?> GetRestaurantById(int id)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.Id == id);
        if(restaurant == null) return null;
        return _mapper.Map<ReadRestaurantDto>(restaurant);
    }

    public async Task<ReadRestaurantDto?> UpdateRestaurant(int id, UpdateRestaurantDto restaurantDto)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.Id == id);
        if(restaurant == null) return null;

        _mapper.Map(restaurantDto, restaurant);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReadRestaurantDto>(restaurant);
    }

    public async Task<ReadRestaurantDto?> PatchUpdateRestaurant(int id,
        JsonPatchDocument<UpdateRestaurantDto> patch)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.Id == id);
        if(restaurant == null) return null;

        var restaurantToUpdate = _mapper.Map<UpdateRestaurantDto>(restaurant);

        patch.ApplyTo(restaurantToUpdate);

        _mapper.Map(restaurantToUpdate, restaurant);

        await _context.SaveChangesAsync();

        return _mapper.Map<ReadRestaurantDto>(restaurant);
    }

    public async Task<ReadRestaurantDto?> DeleteRestaurant(int id)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(restaurant => restaurant.Id == id);
        if(restaurant == null) return null;
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReadRestaurantDto>(restaurant);
    }
}
