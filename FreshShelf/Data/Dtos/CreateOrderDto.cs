using FreshShelf.Models.Enums;
using FreshShelf.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class CreateOrderDto
{
    [Required]
    public int RestaurantId { get; set; }

    [Required]
    public int SupplierId { get; set; }


    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
