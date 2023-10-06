using Ardalis.SharedKernel;
using ChronoPost.Core;
using ChronoPost.Core.Aggregates;
using FluentAssertions;
using NetArchTest.Rules;

namespace ChronoPost.ArchitecturalTests;

[TestFixture]
public class CoreTests
{
    [Test]
    public void CoreDoesNotDependsOnProjects()
    {
        var result = Types.InAssembly(AssemblyReference.Assembly)
            .ShouldNot()
            .HaveDependencyOnAll(UseCases.AssemblyReference.Assembly.GetName().Name,
                Infrastructure.AssemblyReference.Assembly.GetName().Name,
                Api.AssemblyReference.Assembly.GetName().Name)
            .GetResult();   
        result.IsSuccessful
            .Should()
            .BeTrue();
    }
    
    [Test]
    public void CoreAggregatesShouldBeSealed()
    {
        var result= Types.InAssembly(AssemblyReference.Assembly)
            .That()
            .ResideInNamespace(typeof(User).Namespace)
            .Should()
            .BeSealed()
            .GetResult();
        
        result.IsSuccessful
            .Should()
            .BeTrue();
    }
    
}