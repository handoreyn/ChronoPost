using System.Reflection;

namespace ChronoPost.UseCases;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}