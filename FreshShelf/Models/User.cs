using Microsoft.AspNetCore.Identity;

namespace FreshShelf.Models;

public class User : IdentityUser
{
    public DateTime BirthDate { get; set; }
    public User(): base() { }
}
