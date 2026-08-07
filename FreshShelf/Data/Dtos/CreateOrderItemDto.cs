using FreshShelf.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class CreateOrderItemDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
