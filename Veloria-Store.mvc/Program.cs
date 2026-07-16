using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Veloria_Store.Application.DependencyInjection;
using Veloria_Store.Infrastructure.Data;
using Veloria_Store.Infrastructure.Data.Identity;
using Veloria_Store.Infrastructure.DependencyInjection;



namespace Veloria_Store.mvc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("log/log.xt", rollingInterval: RollingInterval.Day).CreateLogger();
            builder.Host.UseSerilog();
            Log.Logger.Information("Application is building ..............");

            // Register Services
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            try
            {
                var app = builder.Build();

                // seed data
              
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    await IdentitySeeder.SeedAsync(services);
                }


                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

             app.UseExceptionHandlerMiddleWare();

            app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
             app.UseAuthentication();

            app.UseAuthorization();
            app.MapStaticAssets();

             Log.Logger.Information("Application is running ..............");


                app.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

                app.MapRazorPages();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Application failed to start ..........");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
