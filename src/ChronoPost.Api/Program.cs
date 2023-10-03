using ChronoPost.Infrastructure;
using ChronoPost.UseCases;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.RegisterInfraServices(builder.Configuration);
builder.Services.AddUseCases();

var app = builder.Build();

app.MapDefaultControllerRoute();
app.Run();
