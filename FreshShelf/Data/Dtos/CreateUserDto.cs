using System.ComponentModel.DataAnnotations;

namespace FreshShelf.Data.Dtos;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
    public string Role { get; set; }
}
