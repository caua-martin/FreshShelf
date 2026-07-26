using AutoMapper;
using FreshShelf.Data.Dtos;
using FreshShelf.Models;

namespace FreshShelf.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierDto, Supplier>();
        CreateMap<Supplier, ReadSupplierDto>();
        CreateMap<UpdateSupplierDto, Supplier>();
    }
}
