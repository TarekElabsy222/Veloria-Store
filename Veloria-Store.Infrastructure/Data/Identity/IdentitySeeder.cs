using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Veloria_Store.Infrastructure.Utilities;

namespace Veloria_Store.Infrastructure.Data.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roles

            if (!await roleManager.RoleExistsAsync(SD.AdminRole))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(SD.AdminRole));
            }

            if (!await roleManager.RoleExistsAsync(SD.UserRole))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(SD.UserRole));
            }

            // Admin Account

            const string email = "admin@veloria.com";

            var admin =
                await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(  admin,  "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        admin,
                        SD.AdminRole);
                }
            }
        }
    }
}
