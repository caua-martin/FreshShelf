using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using FreshShelf.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    public async Task<IActionResult> AddOrderItem(int orderId, [FromBody] CreateOrderItemDto orderItemDto)
    {
        var orderItem = await _orderItemService.AddOrderItem(orderId, orderItemDto);
        if(orderItem == null) return NotFound();
        return CreatedAtAction(nameof(GetOrderItemById),
            new {id = orderItem.Id},
            orderItem);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderItems()
    {
        var items = await _orderItemService.GetOrderItems();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItemById(int id)
    {
        var items = await _orderItemService.GetOrderItemById(id);
        if (items == null) return NotFound();
        return Ok(items);
    }

    [HttpGet("{orderId}/Items")]
    public async Task<IActionResult> GetItemsOfOrders(int orderId)
    {
        var items = await _orderItemService.GetItemsOfOrders(orderId);
        if (items == null) return NotFound();
        return Ok(items);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, UpdateOrderItemDto updateOrderItemDto)
    {
        var items = await _orderItemService.UpdateOrderItem(id, updateOrderItemDto);
        if (items == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var items = await _orderItemService.DeleteOrderItem(id);
        if (items == null) return NotFound();
        return NoContent();
    }
}
