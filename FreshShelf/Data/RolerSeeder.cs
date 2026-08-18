using FreshShelf.Models;
using Microsoft.AspNetCore.Identity;

namespace FreshShelf.Data;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(
        IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider
            .GetRequiredService<RoleManager<AcessProfile>>();

        string[] roles =
        {
            "Admin",
            "Restaurant",
            "Supplier"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new AcessProfile { Name = role }
                );
            }
        }
    }
}