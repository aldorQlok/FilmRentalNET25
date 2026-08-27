using FilmRentalNET25.Models;
using Microsoft.AspNetCore.Identity;

namespace FilmRentalNET25.Service
{
    public static class DataSeeding
    {
        public static async Task SeedAdminUser(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var admin = await userManager.FindByEmailAsync("admin@filmrental.se");

            if(admin != null)
            {
                return;
            }

            admin = new User
            {
                UserName = "admin@filmrental.se",
                Email = "admin@filmrental.se",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, "Admin123!");

            await userManager.AddToRoleAsync(admin, "Admin");

        }
    }
}
