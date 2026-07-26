using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;

namespace FreshShelf.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<UpdateRestaurantDto, Restaurant>();
        CreateMap<Restaurant, UpdateRestaurantDto>();
        CreateMap<Restaurant, ReadRestaurantDto>();
    }
}
