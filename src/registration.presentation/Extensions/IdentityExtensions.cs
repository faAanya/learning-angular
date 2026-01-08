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
            .AddRoles<IdentityRole>()
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

            options.AddPolicy("HasLibraryId", policy=> policy.RequireClaim("LibraryId"));
            options.AddPolicy("FemaleOnly", policy=> policy.RequireClaim("Gender", "Female"));
            options.AddPolicy("Under18", policy=> policy.RequireAssertion(context=>
            int.Parse(context.User.Claims.First(x=>x.Type == "Age").Value) < 18));
        });
        return services;
    }

   public static IServiceCollection AddIdentityAuth(
    this IServiceCollection services,
    IConfiguration configuration)
{
    var jwtSecret = configuration["AppSettings:JWTSecret"];

    if (string.IsNullOrWhiteSpace(jwtSecret))
        throw new InvalidOperationException("JWTSecret is not configured");

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

    return services;
}
}