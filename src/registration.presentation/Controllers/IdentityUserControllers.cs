using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using registration.presentation.Dto;
using registration.presentation.Options;

namespace registration.presentation.Controllers;

public static class IdentityUserControllers
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("signup", async (
            UserManager<AppUser> userManager,
            [FromBody] UserRegistrationDto registrationModel
        ) =>
        {
            var newUser = new AppUser()
            {
                Email = registrationModel.Email,
                UserName = registrationModel.Email,
                Fullname = registrationModel.FullName,
                Gender = registrationModel.Gender,
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-registrationModel.Age)),
                LibraryId =  registrationModel.LibraryId
            };
            var result = await userManager.CreateAsync(newUser, registrationModel.Password);

            if (!await userManager.IsInRoleAsync(newUser, registrationModel.Role))
            {
                await userManager.AddToRoleAsync(newUser, registrationModel.Role);
            }
            
            if (result.Succeeded)
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.BadRequest(result);
            }
        }).AllowAnonymous();

        app.MapPost("signin", async (
            UserManager<AppUser> userManager,
            [FromBody] UserLoginDto loginModel,
            IOptions<AppSettings> options
        ) =>
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var roles = await userManager.GetRolesAsync(user);
                var signingKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.JWTSecret));
                
                var claims = new ClaimsIdentity(
                    [
                        new Claim("userId", user.Id),
                        new Claim("gender", user.Gender.ToString()),
                        new Claim("age", (DateTime.UtcNow.Year - user.DateOfBirth.Year).ToString()),
                        new Claim(ClaimTypes.Role, roles.First()),
                    ]);

                if(user.LibraryId != null)
                {
                    claims.AddClaim(new Claim("libraryId", user.LibraryId.ToString()!));
                }

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(securityToken);
                return Results.Ok(new { token });
            }
            else
            {
                return Results.BadRequest(new { message = "username or password is incorrect" });
            }
        }).AllowAnonymous();
        return app;
    }
}