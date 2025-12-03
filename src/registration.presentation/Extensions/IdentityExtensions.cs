using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace registration.presentation.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityHandlers(this IServiceCollection services)
    {
        services
            .AddIdentityApiEndpoints<AppUser>()
            .AddEntityFrameworkStores<AppDbContext>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
    {
        services
            .Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        });

        services.AddAuthentication();
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
        return services;
    }

    public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = 
                x.DefaultChallengeScheme = 
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["AppSettings:JWTSecret"]!)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
        
        return services;
    }
}