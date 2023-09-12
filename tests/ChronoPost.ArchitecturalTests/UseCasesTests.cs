using FluentAssertions;
using NetArchTest.Rules;

namespace ChronoPost.ArchitecturalTests;

[TestFixture]
public class UseCasesTests
{
    [Test]
    public void UseCasesDependsOnCore()
    {
        
        var result = Types.InAssembly(UseCases.AssemblyReference.Assembly)
            .Should()
            .HaveDependencyOn(Core.AssemblyReference.Assembly.GetName().Name)
            .GetResult();
        
        result.IsSuccessful
            .Should()
            .BeTrue();
    }

    [Test]
    public void UseCasesDoesNotDependsOnInfrastructure()
    {
        var result = Types.InAssembly(UseCases.AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOn(Infrastructure.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful
            .Should()
            .BeTrue();
    }
}