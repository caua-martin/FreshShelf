using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace FreshShelf.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    
    public async Task Register(CreateUserDto dto)
    {
        User user = _mapper.Map<User>(dto);

        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
        
        if (!result.Succeeded)
        {
            throw new ApplicationException("Fail to register user!");
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

        if (!roleResult.Succeeded)
        {
            throw new ApplicationException("Fail to assign role!");
        }
    }

    public async Task<string> Login(LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Fail to sign in user!");
        }

        var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

        var token = await _tokenService.GenerateToken(user);

        return token;
    }
}