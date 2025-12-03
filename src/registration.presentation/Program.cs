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

app.ConfigureSwaggerExplorer();
app.ConfigureCors(builder.Configuration);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<AppUser>();
app.MapIdentityEndpoints()
    .MapAccountEndpoints();
app.Run();
