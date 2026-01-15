using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace registration.presentation.Controllers;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/userProfile", GetUserProfile);
        
        return endpoints;
    }

    [Authorize]
    private static async Task<IResult> GetUserProfile(ClaimsPrincipal user,
        UserManager<AppUser> userManager)
    {
        string userId = user.Claims.First(x => x.Type == "userId").Value;
       
        var userDetails = await userManager.FindByIdAsync(userId);
        
        return Results.Ok(new
        {
            Email = userDetails?.Email,
            FullName = userDetails?.Fullname,
        });
    }
}