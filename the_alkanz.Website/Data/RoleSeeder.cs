using Microsoft.AspNetCore.Identity;
using the_alkanz.Website.Models;

namespace the_alkanz.Website.Data;

public static class RoleSeeder
{
    public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        }
        var adminEmail = "SufiyeAdmin@gmail.com";
        var adminPassword = "Sufiye12!";

        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Sufiye",
                LastName = "Huseynzade",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var result = await userManager.CreateAsync(admin,adminPassword);
            if(result.Succeeded) await userManager.AddToRoleAsync(admin,"Admin");
        }


    }

}
