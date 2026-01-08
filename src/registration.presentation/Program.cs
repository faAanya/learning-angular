using Microsoft.EntityFrameworkCore;
using registration.presentation.Controllers;
using registration.presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerExplorer()
                .InjectDbContext(builder.Configuration)
                .AddAppConfiguration(builder.Configuration)
                .AddIdentityHandlers()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}


app.ConfigureSwaggerExplorer();
app.ConfigureCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<AppUser>();
app.MapIdentityEndpoints()
    .MapAccountEndpoints()
    .MapAuthorizationEndpoints();
await app.RunAsync();
