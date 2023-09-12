using FluentAssertions;
using NetArchTest.Rules;

namespace ChronoPost.ArchitecturalTests;

[TestFixture]
public class CoreTests
{
    [Test]
    public void CoreDoesNotDependsOnProjects()
    {
        var result = Types.InAssembly(Core.AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOnAll(UseCases.AssemblyReference.Assembly.GetName().Name,
                Infrastructure.AssemblyReference.Assembly.GetName().Name,
                Api.AssemblyReference.Assembly.GetName().Name)
            .GetResult();   
        result.IsSuccessful
            .Should()
            .BeTrue();
    }
}