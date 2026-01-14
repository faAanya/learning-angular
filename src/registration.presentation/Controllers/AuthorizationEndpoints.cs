using Microsoft.AspNetCore.Authorization;

namespace registration.presentation.Controllers
{
    public static class AuthorizationEndpoints
    {
        public static IEndpointRouteBuilder MapAuthorizationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/admin-only", 
            [Authorize(Roles = "ADMIN")] () =>
            {
                return "AdminOnly";
            });

            app.MapGet("/admin-or-teacher", 
            [Authorize(Roles = "Admin, Teacher")] () =>
            {
                return "Admin or teacher";
            });

            app.MapGet("/library-member-only", 
            [Authorize(Policy = "HasLibraryId")] () =>
            {
                return "Library Member Only";
            });

            app.MapGet("/under-18", 
            [Authorize(Policy = "Under18")] () =>
            {
                return "Under 18";
            });

            return app;
        }
    }
}