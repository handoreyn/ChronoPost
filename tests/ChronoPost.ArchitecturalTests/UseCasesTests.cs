using ChronoPost.Core.Aggregates;
using ChronoPost.UseCases;
using ChronoPost.UseCases.Users.FindUserById;
using FluentAssertions;
using NetArchTest.Rules;

namespace ChronoPost.ArchitecturalTests;

[TestFixture]
public class UseCasesTests
{

    [Test]
    public void UseCasesDoesNotDependsOnInfrastructure()
    {
        var useCaseAssembly = UseCases.AssemblyReference.Assembly;
        var infraAssemblyName = Infrastructure.AssemblyReference.Assembly.GetName().Name;
        
        var result = Types.InAssembly(useCaseAssembly)
            .ShouldNot()
            .HaveDependencyOn(infraAssemblyName)
            .GetResult();

        result.IsSuccessful
            .Should()
            .BeTrue();
    }

    [Test]
    public void UseCaseHandlersAndParametersShouldBeSealed()
    {
        var result = Types.InAssembly(AssemblyReference.Assembly)
            .That()
            .ResideInNamespace("ChronoPost.UseCases")
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful
            .Should()
            .BeTrue();
    }
}