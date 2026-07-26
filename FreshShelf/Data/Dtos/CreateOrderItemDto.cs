using FreshShelf.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class CreateOrderItemDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal PriceAtPurchase { get; set; }


    public decimal SubTotal => Quantity * PriceAtPurchase;
}
