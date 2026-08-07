using FreshShelf.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class UpdateOrderItemDto
{
    [Required]
    public int Quantity { get; set; }
}
