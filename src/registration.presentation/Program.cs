using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityApiEndpoints<AppUser>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("db"));
});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });

app.MapControllers();
app.MapIdentityApi<AppUser>();

app.MapPost("signup", async (
UserManager<AppUser> userManager,
[FromBody] UserRegistrationModel userRegistrationModel
) =>
{
    var newUser = new AppUser()
    {
        Email = userRegistrationModel.Email,
        UserName = userRegistrationModel.Email,
        Fullname = userRegistrationModel.FullName
    };
    var result = await userManager.CreateAsync(newUser, userRegistrationModel.Password);

    if (result.Succeeded)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.BadRequest(result);
    }
    
});

app.Run();

public class UserRegistrationModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
}