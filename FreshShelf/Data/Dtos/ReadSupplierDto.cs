using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class ReadSupplierDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
}
