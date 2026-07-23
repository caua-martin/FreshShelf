using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Order
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    [Required]
    public int SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }

    //public decimal DeliveryPrice { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}