using FluentAssertions;
using NetArchTest.Rules;

namespace ChronoPost.ArchitecturalTests;

[TestFixture]
public class InfrastructureTests
{
    [Test]
    public void InfrastructureDependsOnUseCasesAndCore()
    {
        var result = Types.InAssembly(Infrastructure.AssemblyReference.Assembly)
            .Should()
            .HaveDependencyOnAll(UseCases.AssemblyReference.Assembly.GetName().Name,
                Core.AssemblyReference.Assembly.GetName().Name)
            .GetResult();

        result.IsSuccessful
            .Should()
            .BeTrue();
    }
}