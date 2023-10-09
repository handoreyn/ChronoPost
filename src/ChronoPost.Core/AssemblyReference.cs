using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoPost.Core;

public static class AssemblyReference
{
    public static Assembly Assembly = Assembly.GetExecutingAssembly();

    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly));
    }


}