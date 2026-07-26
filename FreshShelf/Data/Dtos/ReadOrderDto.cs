using FreshShelf.Models.Enums;
using FreshShelf.Models;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class ReadOrderDto
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }
    public string RestaurantName {  get; set; }
    
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }


    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
