using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            app.MapGet("/female-techer-only", 
            [Authorize(Policy = "FemaleOnly", 
            Roles = "Teacher")] () =>
            {
                return "Library Member Only";
            });

            app.MapGet("/under-18", 
            [Authorize(Policy = "FemaleOnly")] 
            [Authorize(Policy = "Under18")] () =>
            {
                return "Library Member Only";
            });

            return app;
        }
    }
}