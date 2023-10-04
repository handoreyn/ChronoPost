using ChronoPost.Api.Extensions;
using ChronoPost.Infrastructure;
using ChronoPost.UseCases;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddJwt(builder.Configuration);
builder.Services.RegisterInfraServices(builder.Configuration);
builder.Services.AddUseCases();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
