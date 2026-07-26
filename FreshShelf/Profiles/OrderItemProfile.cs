using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;

namespace FreshShelf.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<CreateOrderItemDto, OrderItem>();
        CreateMap<OrderItem, UpdateOrderItemDto>();
        CreateMap<UpdateOrderItemDto, OrderItem>();
        CreateMap<OrderItem, ReadOrderItemDto>();
    }  
}