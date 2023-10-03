using System.Reflection;
using Ardalis.SharedKernel;
using ChronoPost.Core.Aggregates;
using ChronoPost.Infrastructure.Persistence.Data;
using ChronoPost.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoPost.Infrastructure;

public static class AssemblyReference
{
    public static Assembly Assembly = Assembly.GetExecutingAssembly();

    public static void RegisterInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChronoPostDatabaseContext>(opt => opt
            .UseSqlServer(configuration.GetConnectionString("ChronoPostSqlConnectionString")));

        services.AddScoped<IReadRepository<User>, ReadRepository<User>>();
    }
}