using FreshShelf.Data.Dtos;
using FreshShelf.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController : ControllerBase
{
    public readonly OrderItemService _orderItemService;

    public OrderItemController(OrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpPost("{orderId}/Items")]
    [Authorize(Roles = "Restaurant,Admin")]
    public async Task<IActionResult> AddOrderItem(int orderId, [FromBody] CreateOrderItemDto orderItemDto)
    {
        var orderItem = await _orderItemService.AddOrderItem(orderId, orderItemDto);
        if(orderItem == null) return NotFound();
        return CreatedAtAction(nameof(GetOrderItemById),
            new {id = orderItem.Id},
            orderItem);
    }

    [HttpGet]
    [Authorize(Roles = "Restaurant,Admin")]
    public async Task<IActionResult> GetOrderItems()
    {
        var items = await _orderItemService.GetOrderItems();
        return Ok(items);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    public async Task<IActionResult> GetOrderItemById(int id)
    {
        var items = await _orderItemService.GetOrderItemById(id);
        if (items == null) return NotFound();
        return Ok(items);
    }

    [HttpGet("{orderId}/Items")]
    [Authorize(Roles = "Restaurant,Admin")]
    public async Task<IActionResult> GetItemsOfOrders(int orderId)
    {
        var items = await _orderItemService.GetItemsOfOrders(orderId);
        if (items == null) return NotFound();
        return Ok(items);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    public async Task<IActionResult> UpdateOrderItem(int id, UpdateOrderItemDto updateOrderItemDto)
    {
        var items = await _orderItemService.UpdateOrderItem(id, updateOrderItemDto);
        if (items == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Restaurant,Admin")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var items = await _orderItemService.DeleteOrderItem(id);
        if (items == null) return NotFound();
        return NoContent();
    }
}