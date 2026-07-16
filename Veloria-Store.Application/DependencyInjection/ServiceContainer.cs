using Microsoft.Extensions.DependencyInjection;
using Veloria_Store.Application.Mapping;
using Veloria_Store.Application.Services.Implementations;
using Veloria_Store.Application.Services.Interfaces;

namespace Veloria_Store.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // register mapping
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            // register services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
