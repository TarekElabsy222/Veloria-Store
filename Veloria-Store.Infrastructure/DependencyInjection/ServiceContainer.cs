using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Veloria_Store.Application.Services.Interfaces.Cookies;
using Veloria_Store.Application.Services.Interfaces.Logging;
using Veloria_Store.Domain.Repositories.Interfaces;
using Veloria_Store.Infrastructure.Data;
using Veloria_Store.Infrastructure.MiddleWare;
using Veloria_Store.Infrastructure.Repositories.Implementation;
using Veloria_Store.Infrastructure.Services;


namespace Veloria_Store.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            // register AppDbcontext
            services.AddDbContext<AppDbContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("Default"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();
                }).UseExceptionProcessor(),
                ServiceLifetime.Scoped);



            // register serilog
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));



            // register services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICookieCartService, CookieCartService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            return services;
        }
        public static IApplicationBuilder UseExceptionHandlerMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleWare>();
            return app;
        }
    }
}
