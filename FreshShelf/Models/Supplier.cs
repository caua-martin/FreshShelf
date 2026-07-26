using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Supplier
{
    [Key]
    [Required]
    public int Id { get; set; }
    // Futuramente usar asp.net identity ou similar
    //public string UserId { get; set; } = string.Empty;
    [Required(ErrorMessage = "O campo de nome e obrigatorio")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Cnpj { get; set; } = string.Empty;
    [Required]
    public string Region { get; set; } = string.Empty;
    public virtual ICollection<Order> Orders { get; set; }
    //public SupplierStatus Status { get; set; } = SupplierStatus.Approved;
    public virtual ICollection<Product> Products { get; set; }
    //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
