using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using FreshShelf.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddOrder([FromBody] CreateOrderDto orderDto)
    {
        var order = await _orderService.AddOrder(orderDto);
        return CreatedAtAction(nameof(GetOrderById),
            new { id = order.Id},
            order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetOrders();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto orderDto)
    {
        var order = await _orderService.UpdateDto(id, orderDto);
        if (order == null) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUpdateOrder(int id, JsonPatchDocument<UpdateOrderDto> patch)
    {
        var order = await _orderService.PatchUpdateOrder(id, patch);
        if (order == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.DeleteOrder(id);
        if (order == null) return NotFound();
        return NoContent();
    }
}