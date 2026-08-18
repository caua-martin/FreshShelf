using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Models;

public class Supplier
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; } = string.Empty;
    [Required(ErrorMessage = "You must write the name")]
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
