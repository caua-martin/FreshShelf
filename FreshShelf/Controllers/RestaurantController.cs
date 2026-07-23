using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private ProductContext _context;
    private IMapper _mapper;

    public RestaurantController(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddRestaurant([FromBody] CreateRestaurantDto restaurantDto)
    {
        Restaurant restaurant = _mapper.Map<Restaurant>(restaurantDto);
        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetRestaurantById),
            new { id = restaurant.Id },
            restaurant);
    }

    [HttpGet]
    public IEnumerable<ReadRestaurantDto> GetRestaurants()
    {
        return _mapper.Map<List<ReadRestaurantDto>>(_context.Restaurants.ToList());
    }

    [HttpGet("range")]
    public IEnumerable<ReadRestaurantDto> GettingRangeRestaurants([FromQuery] int skip, [FromQuery] int take)
    {
        return _mapper.Map<List<ReadRestaurantDto>>(_context.Restaurants.Skip(skip).Take(take).ToList());
    }

    [HttpGet("id")]
    public IActionResult GetRestaurantById(int id)
    {
        var restaurant = _context.Restaurants.FirstOrDefault(restaurant => restaurant.Id == id);
        if(restaurant == null) return NotFound();
        var restaurantDto = _mapper.Map<ReadRestaurantDto>(restaurant);
        return Ok(restaurantDto);
    }
}