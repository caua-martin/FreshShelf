using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class UpdateProductDto
{
    [Required(ErrorMessage = "O nome e obrigatorio")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "A descricao e obrigatoria")]
    [StringLength(200, ErrorMessage = "A descricacao nao pode" +
        "exceder 200 caracteres")]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Range(1, 40000, ErrorMessage = "O preco deve ser entre 0.1" +
        "a 40000 reais")]
    public decimal Price { get; set; }
}
