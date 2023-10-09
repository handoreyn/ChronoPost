using ChronoPost.Api.Extensions;
using ChronoPost.Infrastructure;
using ChronoPost.UseCases;
using ChronoPost.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddJwt(builder.Configuration);
builder.Services.RegisterInfraServices(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddUseCases();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("ChronoPostRedisConnectionString");
    options.InstanceName = "SampleInstance";
});


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
