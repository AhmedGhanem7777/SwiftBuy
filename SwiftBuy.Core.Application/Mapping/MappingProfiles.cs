using AutoMapper;
using SwiftBuy.Core.Application.Abstraction.Models;
using SwiftBuy.Core.Application.Abstraction.Models.Order;
using SwiftBuy.Core.Domain.Common.Entities;
using SwiftBuy.Core.Domain.Entities.Basket;
using SwiftBuy.Core.Domain.Entities.Order;
using SwiftBuy.Core.Domain.Entities.Product;
using CustomerBasketDto = SwiftBuy.Shared.Models.Basket.CustomerBasketDto;
using BasketItemDto = SwiftBuy.Shared.Models.Basket.BasketItemDto;
using OrderAddress = SwiftBuy.Core.Domain.Entities.Order.Address;
using UserAddress = SwiftBuy.Core.Domain.Entities.Identity.Address;

namespace SwiftBuy.Core.Application.Mapping
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category!.Name))
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand!.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<BrandPictureUrlResolver>() );

            CreateMap<ProductCategory, CategoryDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<CategoryPictureUrlResolver>());

            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<OrderAddress, AddressDto>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<UserAddress, AddressDto>().ReverseMap();

        }
    }
}
