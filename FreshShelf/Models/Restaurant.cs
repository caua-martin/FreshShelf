using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Restaurant
{
    public int Id { get; set; }
    // Futuramente usar asp.net identity ou similar
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public RestaurantStatus Status { get; set; } = RestaurantStatus.Pending;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
