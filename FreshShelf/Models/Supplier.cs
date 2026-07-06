using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Supplier
{
    public int Id { get; set; }
    // Futuramente usar asp.net identity ou similar
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;


    public SupplierStatus Status { get; set; } = SupplierStatus.Pending;
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
