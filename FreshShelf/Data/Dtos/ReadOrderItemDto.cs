using FreshShelf.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class ReadOrderItemDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }


    public int Quantity { get; set; }

    public decimal PriceAtPurchase { get; set; }


    public decimal SubTotal => Quantity * PriceAtPurchase;
}
