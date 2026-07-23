using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class ReadProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SupplierId { get; set; }
}