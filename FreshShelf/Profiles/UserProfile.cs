using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;

namespace FreshShelf.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}
