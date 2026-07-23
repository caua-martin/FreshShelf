using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class CreateSupplierDto
{
    [Required(ErrorMessage = "O campo de nome e obrigatorio")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Cnpj { get; set; } = string.Empty;
    [Required]
    public string Region { get; set; } = string.Empty;
}
