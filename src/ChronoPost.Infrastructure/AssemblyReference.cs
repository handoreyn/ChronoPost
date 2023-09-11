using System.Reflection;

namespace ChronoPost.Infrastructure;

public static class AssemblyReference
{
    public static Assembly Assembly = Assembly.GetExecutingAssembly();
}