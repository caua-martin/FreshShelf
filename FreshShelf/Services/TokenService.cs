using FreshShelf.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FreshShelf.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }


        var key = _configuration["SymmetricSecurityKey"]
                        ?? throw new Exception("Chave JWT não configurada");

        var chave = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256); 

        var token = new JwtSecurityToken ( expires: DateTime.Now.AddMinutes(10), claims: claims, signingCredentials: signingCredentials ); 
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}