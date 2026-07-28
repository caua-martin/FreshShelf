using FreshShelf.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FreshShelf.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", user.UserName),
            new Claim("id", user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        var key = _configuration["SymmetricSecurityKey"]
                        ?? throw new Exception("Chave JWT não configurada");

        var chave = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256); 

        var token = new JwtSecurityToken ( expires: DateTime.Now.AddMinutes(10), claims: claims, signingCredentials: signingCredentials ); 
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}