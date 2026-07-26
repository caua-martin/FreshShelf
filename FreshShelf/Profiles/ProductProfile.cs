using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;

namespace FreshShelf.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, UpdateProductDto>();
        CreateMap<Product, ReadProductDto>();
    }
}
