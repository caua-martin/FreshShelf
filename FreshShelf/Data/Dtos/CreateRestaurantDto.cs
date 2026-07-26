using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class CreateRestaurantDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Cnpj { get; set; } = string.Empty;
    [Required]
    public string Region { get; set; } = string.Empty;
}
