using AutoMapper;
using FreshShelf.Data;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private ProductContext _context;
    private IMapper _mapper;

    public OrderController(ProductContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddOrder([FromBody] CreateOrderDto orderDto)
    {
        Order order = _mapper.Map<Order>(orderDto);
        _context.Orders.Add(order);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetOrderById),
            new { id = order.Id},
            order);
    }

    [HttpGet]
    public IEnumerable<ReadOrderDto> GetOrders()
    {
        return _mapper.Map<List<ReadOrderDto>>(_context.Orders.ToList());
    }

    [HttpGet("range")]
    public IEnumerable<ReadOrderDto> GettingRangeOrders([FromQuery] int skip, [FromQuery] int  take)
    {
        return _mapper.Map<List<ReadOrderDto>>(_context.Orders.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetOrderById(int id)
    {
        var order = _context.Orders.FirstOrDefault(x => x.Id == id);
        if (order == null) return NotFound();
        var orderDto = _mapper.Map<ReadOrderDto>(order);
        return Ok(orderDto);
    }

    
}
