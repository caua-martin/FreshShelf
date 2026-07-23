using FreshShelf.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Models;

public class Restaurant
{
    [Key]
    [Required]
    public int Id { get; set; }

    // Futuramente usar asp.net identity ou similar
    //public string UserId { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Cnpj { get; set; } = string.Empty;
    [Required]
    public string Region { get; set; } = string.Empty;

    public virtual ICollection<Order> Orders { get; set; }

    //public RestaurantStatus Status { get; set; } = RestaurantStatus.Pending;
    //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
