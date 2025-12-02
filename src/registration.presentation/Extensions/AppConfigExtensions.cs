using registration.presentation.Options;

namespace registration.presentation.Extensions;

public static class AppConfigExtensions
{
    public static WebApplication ConfigureCors(this WebApplication app, IConfiguration configuration)
    {
        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        return app;
    }

    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        return services;
    }
}