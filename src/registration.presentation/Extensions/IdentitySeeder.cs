using Microsoft.AspNetCore.Identity;

namespace registration.presentation.Extensions
{public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        string[] roles = { "ADMIN", "USER", "TEACHER" };

foreach (var roleName in roles)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        var role = new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = roleName,
            NormalizedName = roleName.ToUpperInvariant()
        };

        await roleManager.CreateAsync(role);
    }
}
}
        }
}