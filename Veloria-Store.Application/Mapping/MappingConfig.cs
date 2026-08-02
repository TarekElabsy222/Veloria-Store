using AutoMapper;
using Veloria_Store.Application.ViewModels.BrandViewModel;
using Veloria_Store.Application.ViewModels.CategoryViiewModels;
using Veloria_Store.Application.ViewModels.Checkout;
using Veloria_Store.Application.ViewModels.HomeViewModel;
using Veloria_Store.Application.ViewModels.Order;
using Veloria_Store.Application.ViewModels.ProductViewModels;
using Veloria_Store.Domain.Entities;

namespace Veloria_Store.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            // mapping product
            CreateMap<Product, ProductCardVM>().ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                 .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Take(2).Select(i => i.ImageUrl).ToList()));

            CreateMap<Product, ProductDetailsVM>().ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.ImageUrl)));

            CreateMap<Product, ProductVM>().ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                                           .ForMember(d => d.BrandName,o => o.MapFrom(s => s.Brand.Name))
                                           .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<ProductCreateVM, Product>()
    .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
    .ForMember(dest => dest.SoldCount, opt => opt.Ignore())
    .ForMember(dest => dest.Images, opt => opt.Ignore())
    .ReverseMap()
    .ForMember(dest => dest.Images, opt => opt.Ignore())
    .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<Product, ProductUpdateVM>()
                .ForMember(d => d.ImageUrls,
                    o => o.MapFrom(s => s.Images.Select(i => i.ImageUrl).ToList()))
                .ForMember(d => d.Images,
                    o => o.Ignore());

            CreateMap<ProductUpdateVM, Product>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Images, o => o.Ignore());

            CreateMap<Product, ProductShowCaseVM>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).FirstOrDefault()));
            // mapping category
            CreateMap<Category, CategoryVM>();
            CreateMap<Category, CategoryShopVM>();

            // mapping brand
            CreateMap<Brand, BrandVM>();
            // Create
            CreateMap<BrandCreateVM, Brand>();

            // Update
            CreateMap<BrandUpdateVM, Brand>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore());

            // mapping Checkout
            CreateMap<CheckoutVM, Order>()
                    .ForMember(d => d.Id, opt => opt.Ignore())
                     .ForMember(d => d.OrderItems, opt => opt.Ignore())
                      .ForMember(d => d.OrderNumber, opt => opt.Ignore())
                      .ForMember(d => d.Status, opt => opt.Ignore())
                       .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                      .ForMember(d => d.SubTotal, opt => opt.Ignore())
                      .ForMember(d => d.ShippingCost, opt => opt.Ignore())
                      .ForMember(d => d.Total, opt => opt.Ignore())
                       .ForMember(d => d.UserId, opt => opt.Ignore());

            CreateMap<CartItem, CheckoutItemVM>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name))
                .ForMember(d => d.ProductImage, opt => opt.MapFrom(s => s.Product.Images
                        .Select(i => i.ImageUrl).FirstOrDefault()))
                .ForMember(d => d.Price,  opt => opt.MapFrom(s => s.Product.Price));

            CreateMap<CartItem, OrderItem>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.OrderId, opt => opt.Ignore())
                .ForMember(d => d.Order, opt => opt.Ignore())
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name))
                .ForMember(d => d.ProductImage, opt => opt.MapFrom(s => s.Product.Images.Select(i => i.ImageUrl).FirstOrDefault()))
                .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.Product.Price))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity));

            // admin order
            CreateMap<Order, OrderAdminVM>()
                     .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.CustomerName))
                     .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.ToString()));


            CreateMap<Order, OrderAdminDetailsVM>()
                .ForMember(d => d.Items, o => o.MapFrom(s => s.OrderItems));


            CreateMap<OrderItem, OrderItemVM>()
           .ForMember(d => d.ProductId,o => o.MapFrom(s => s.ProductId))
           .ForMember(d => d.ProductName,o => o.MapFrom(s => s.Product.Name))
           .ForMember(d => d.ProductImage, o => o.MapFrom(s => s.Product.Images.Select(i => i.ImageUrl).FirstOrDefault() ?? "/images/no-image.png"))
           .ForMember(d => d.Price, o => o.MapFrom(s => s.UnitPrice))
           .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity));


            CreateMap<OrderStatusUpdateVM, Order>();
            CreateMap<Order, UserOrderVM>().ForMember(d => d.StatusName,o => o.MapFrom(s => s.Status.ToString()));

        }
    }
}