using AutoMapper;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponseDto>();

        CreateMap<CreateProductRequestDto, Product>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(scr => DateTimeOffset.UtcNow))
                       .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateProductRequestDto, Product>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(scr => DateTimeOffset.UtcNow))
                       .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<Category, CategoryResponseDto>();

        CreateMap<CreateCategoryRequestDto, Category>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                       .ForMember(dest => dest.Products, opt => opt.Ignore());

  
    }

  
}
