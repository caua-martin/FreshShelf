using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Models.Enums;

namespace FreshShelf.Models;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome e obrigatorio")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "A descricao e obrigatoria")]
    [MaxLength(200, ErrorMessage = "A descricacao nao pode" +
        "exceder 200 caracteres")]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Range(1, 40000, ErrorMessage = "O preco deve ser entre 0.1" +
        "a 40000 reais")]
    public decimal Price { get; set; }    
//    public int SupplierId { get; set; }
//    public Supplier? Supplier { get; set; }
//    public ProductStatus Status { get; set; } = ProductStatus.Active;
}
