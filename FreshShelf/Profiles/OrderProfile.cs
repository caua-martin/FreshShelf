using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;

namespace FreshShelf.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
        CreateMap<Order, UpdateOrderDto>();
        CreateMap<Order, ReadOrderDto>()
            .ForMember(dto => dto.RestaurantName,
                opt => opt.MapFrom(order => order.Restaurant.Name))
            .ForMember(dto => dto.SupplierName,
                opt => opt.MapFrom(order => order.Supplier.Name));
    }
}
