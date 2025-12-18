using Microsoft.EntityFrameworkCore;

namespace registration.presentation.Extensions;

public static class EfCoreEntension
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}