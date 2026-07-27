using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FreshShelf.Services;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(CreateUserDto dto)
    {
        await _userService.Register(dto);
        return Ok("User has been registered!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUserDto dto)
    {
        var token = await _userService.Login(dto);
        return Ok(token);
    }
}