using Microsoft.AspNetCore.Authorization;

namespace registration.presentation.Controllers;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/UserProfile", GetUserProfile);
        
        return endpoints;
    }

    [Authorize]
    private static string GetUserProfile(HttpContext context)
    {
        return "userProfile";
    }
}