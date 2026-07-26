using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class ReadRestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
}
