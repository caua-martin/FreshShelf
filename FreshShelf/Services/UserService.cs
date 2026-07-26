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

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }
    
    public async Task Register(CreateUserDto dto)
    {
        User user = _mapper.Map<User>(dto);

        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
        
        if (!result.Succeeded)
        {
            throw new ApplicationException("Fail to register user!");
        }
    }

    public async Task Login(LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Fail to sign in user!");
        }
    }
}