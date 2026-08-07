using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Mvc;
using FreshShelf.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantService _restaurantService;

    public RestaurantController(RestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantDto restaurantDto)
    {
        var restaurant = await _restaurantService.AddRestaurant(restaurantDto);

        return CreatedAtAction(nameof(GetRestaurantById),
            new { id = restaurant.Id },
            restaurant);
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurants()
    {
        var restaurants = await _restaurantService.GetRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("range")]
    public async Task<IActionResult> GetRestaurantsRange([FromQuery] int skip, [FromQuery] int take)
    {
        var restaurantsRanged = await _restaurantService.GetRestaurantsRange(skip, take);
        return Ok(restaurantsRanged);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantById(id);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantDto restaurantDto)
    {
        var restaurant = await _restaurantService.UpdateRestaurant(id, restaurantDto);
        if (restaurant == null) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUpdateRestaurant(int id,
    [FromBody] JsonPatchDocument<UpdateRestaurantDto> patch)
    {
        var restaurant = await _restaurantService.PatchUpdateRestaurant(id, patch);
        if(restaurant == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var restaurantDelete = await _restaurantService.DeleteRestaurant(id);
        if(restaurantDelete == null) return NotFound();

        return NoContent();
    }
}