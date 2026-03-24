using AutoMapper;
using the_alkanz.Website.DTOs;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Product

        CreateMap<Product, ProductResponseDto>();

        CreateMap<CreateProductRequestDto, Product>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(scr => DateTimeOffset.UtcNow))
                       .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpdateProductRequestDto, Product>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                       .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(scr => DateTimeOffset.UtcNow))
                       .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        //Category

        CreateMap<Category, CategoryResponseDto>();

        CreateMap<CreateCategoryRequestDto, Category>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore())
                       .ForMember(dest => dest.Products, opt => opt.Ignore());

        //User

        CreateMap<ApplicationUser, AuthResponseDto>();

        //BasketItem

        CreateMap<BasketItem, BasketResponseDto>();

        CreateMap<CreateBasketItemRequestDto, BasketItem>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.UserId, opt => opt.Ignore());

       //Order

        CreateMap<Order, OrderResponseDto>();
        CreateMap<Order, OrderItemResponseDto>();

        CreateMap<OrderStatusChange, Order>()
                .ForMember(d => d.Id, opt => opt.Ignore());

    }

  
}
