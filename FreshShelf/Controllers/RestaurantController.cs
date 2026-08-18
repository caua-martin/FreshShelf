using FreshShelf.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using FreshShelf.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

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

    /// <summary>
    /// Adds a restaurant to the Database.
    /// </summary>
    /// <param name="restaurantDto">The restaurant data to be added</param>
    /// <returns>The newly created restaurant</returns>
    /// <response code="201">The restaurant was successfully created</response>
    [HttpPost]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantDto restaurantDto)
    {
        var restaurant = await _restaurantService.AddRestaurant(restaurantDto);

        return CreatedAtAction(nameof(GetRestaurantById),
            new { id = restaurant.Id },
            restaurant);
    }

    /// <summary>
    /// Gets all restaurants.
    /// </summary>
    /// <returns>A list of all restaurants</returns>
    /// <response code="200">The restaurants were succesffully retrieved</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRestaurants()
    {
        var restaurants = await _restaurantService.GetRestaurants();
        return Ok(restaurants);
    }


    /// <summary>
    /// Gets a range of restaurants.
    /// </summary>
    /// <param name="skip">The number of restaurants that you want to skip</param>
    /// <param name="take">The number of restaurants that you want to retrieve</param>
    /// <returns>A list of all taken restaurants</returns>
    /// <response code="200">The ranged restaurants were successffully retrieved</response>
    [HttpGet("range")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRestaurantsRange([FromQuery] int skip, [FromQuery] int take)
    {
        var restaurantsRanged = await _restaurantService.GetRestaurantsRange(skip, take);
        return Ok(restaurantsRanged);
    }

    /// <summary>
    /// Gets a restaurant by id.
    /// </summary>
    /// <param name="id">The id of the request restaurant</param>
    /// <returns>The chosen restaurant</returns>
    /// <response code="200">The restaurant was succesffully retrieved</response>
    /// <response code="404">The restaurant was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantById(id);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }

    /// <summary>
    /// Updates a restaurant by id.
    /// </summary>
    /// <param name="id">The id of the restaurant to update</param>
    /// <param name="restaurantDto">The restaurant data to be updated</param>
    /// <returns>No content</returns>
    /// <response code="204">The restaurant was successffully updated</response>
    /// <response code="404">The restaurant was not found</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantDto restaurantDto)
    {
        var restaurant = await _restaurantService.UpdateRestaurant(id, restaurantDto);
        if (restaurant == null) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Partially updates a restaurant.
    /// </summary>
    /// <param name="id">The id of the restaurant to update</param>
    /// <param name="patch">The JSON patch document containing the changes to apply</param>
    /// <returns>No content</returns>
    /// <response code="204">The restaurant was successfully updated</response>
    /// <response code="404">The restaurant was not found</response>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchUpdateRestaurant(int id,
    [FromBody] JsonPatchDocument<UpdateRestaurantDto> patch)
    {
        var restaurant = await _restaurantService.PatchUpdateRestaurant(id, patch);
        if(restaurant == null) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Deletes a restaurant.
    /// </summary>
    /// <param name="id">The id of the restaurant to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">The restaurant was successfully deleted</response>
    /// <response code="404">The restaurant was not found</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var restaurantDelete = await _restaurantService.DeleteRestaurant(id);
        if(restaurantDelete == null) return NotFound();

        return NoContent();
    }
}