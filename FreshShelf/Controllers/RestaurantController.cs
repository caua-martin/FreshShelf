using FreshShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private static List<Restaurant> restaurants = new List<Restaurant>();
    private static int nextId = 0;

    [HttpPost]
    public CreatedAtActionResult AddRestaurant([FromBody] Restaurant restaurant)
    {
        restaurant.Id = nextId++;
        restaurants.Add(restaurant);
        return CreatedAtAction(nameof(GetRestaurantById),
            new { id = restaurant.Id },
            restaurant);
    }

    [HttpGet]
    public List<Restaurant> GetRestaurants()
    {
        return restaurants;
    }

    [HttpGet("range")]
    public IEnumerable<Restaurant> GettingRangeRestaurant([FromQuery] int skip, int take)
    {
        return restaurants.Skip(skip).Take(take);
    }

    [HttpGet("id")]
    public IActionResult GetRestaurantById(int id)
    {
        var restaurant = restaurants.FirstOrDefault(x => x.Id == id);
        if(restaurant == null) return NotFound();
        return Ok(restaurant);
    }
}
