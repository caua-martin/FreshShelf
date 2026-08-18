using FreshShelf.Data.Dtos;
using FreshShelf.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Adds an Order to the Database.
    /// </summary>
    /// <param name="orderDto">The order data to be added</param>
    /// <returns>The newly created order</returns>
    /// <response code="201">The order was successfully created</response>
    [HttpPost]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddOrder([FromBody] CreateOrderDto orderDto)
    {
        var order = await _orderService.AddOrder(orderDto);
        return CreatedAtAction(nameof(GetOrderById),
            new { id = order.Id},
            order);
    }

    /// <summary>
    /// Gets all orders.
    /// </summary>
    /// <returns>A list of all orders</returns>
    /// <response code="200">The orders were successfully retrieved</response>
    [HttpGet]
    [Authorize(Roles = "Restaurant,Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetOrders();
        return Ok(orders);
    }

    /// <summary>
    /// Gets an order by id.
    /// </summary>
    /// <param name="id">The id of the requested order</param>
    /// <returns>The requested order</returns>
    /// <response code="200">The order was successfully retrieved</response>
    /// <response code="404">The order was not found</response>
    [HttpGet("{id}")]
    [Authorize(Roles = "Restaurant,Supplier,Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Updates an order by id
    /// </summary>
    /// <param name="id">The id of the requested order</param>
    /// <param name="orderDto">The order data to be updated</param>
    /// <returns>No Content</returns>
    /// <response code="204">The order was successfully updated</response>
    /// <response code="404">The order was not found</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto orderDto)
    {
        var order = await _orderService.UpdateDto(id, orderDto);
        if (order == null) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Partially updates an order
    /// </summary>
    /// <param name="id">The id of the requested order</param>
    /// <param name="patch">The JSON patch document containing the changes to apply</param>
    /// <returns>No Content</returns>
    /// <response code="204">The order was successfully updated</response>
    /// <response code="404">The order was not found</response>
    [HttpPatch("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchUpdateOrder(int id, JsonPatchDocument<UpdateOrderDto> patch)
    {
        var order = await _orderService.PatchUpdateOrder(id, patch);
        if (order == null) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Deletes an order by id.
    /// </summary>
    /// <param name="id">The id of the requested order</param>
    /// <returns>No content</returns>
    /// <response code="204">The order was successfully deleted</response>
    /// <response code="404">The order was not found</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.DeleteOrder(id);
        if (order == null) return NotFound();
        return NoContent();
    }
}