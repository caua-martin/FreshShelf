using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Order
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public decimal ProductsTotal => Items.Sum(item => item.SubTotal);
    public decimal DeliveryPrice { get; set; }
    public decimal TotalPrice => ProductsTotal + DeliveryPrice;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}