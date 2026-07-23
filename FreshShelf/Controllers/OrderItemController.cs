using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController : ControllerBase
{
    public ProductContext _context;
    public IMapper _mapper;

    public OrderItemController(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddOrderItem([FromBody] CreateOrderItemDto orderItemDto)
    {
        OrderItem orderItem = _mapper.Map<OrderItem>(orderItemDto);
        _context.OrderItems.Add(orderItem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetOrderItemById),
            new {id = orderItem.Id},
            orderItem);
    }

    [HttpGet]
    public IEnumerable<ReadOrderItemDto> GetOrderItems()
    {
        return _mapper.Map<List<ReadOrderItemDto>>(_context.OrderItems.ToList());
    }

    [HttpGet("range")]
    public IEnumerable<ReadOrderItemDto> GetRangeOrderItems([FromQuery] int skip, [FromQuery] int take)
    {
        return _mapper.Map<List<ReadOrderItemDto>>(_context.OrderItems.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetOrderItemById(int id)
    {
        var orderItem = _context.OrderItems.FirstOrDefault(orderItem => orderItem.Id == id);
        if (orderItem == null) return NotFound();
        var orderItemDto = _mapper.Map<ReadOrderItemDto>(orderItem);
        return Ok(orderItemDto);
    }
}
