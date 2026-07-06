using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }    
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Active;
}
