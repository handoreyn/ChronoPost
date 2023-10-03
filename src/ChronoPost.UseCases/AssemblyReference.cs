using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoPost.UseCases;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(config=> config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
