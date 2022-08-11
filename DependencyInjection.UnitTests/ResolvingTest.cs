using DependencyInjection.UnitTests.Fake;
using FluentAssertions;

namespace DependencyInjection.UnitTests;

public class ResolvingTest
{
    private readonly IDIFactory _diFactory;
    
    public ResolvingTest()
    {
        _diFactory = new DIFactory();
    }

    [Fact]
    public void TestResolveSingleton()
    {
        _diFactory.AddSingleton<IGetGuidService, GetGuidService>();
        _diFactory.AddTransient<DependentClass>();

        var dependentClass = _diFactory.GetRealisation<DependentClass>();

        dependentClass.First.Should().Be(_diFactory.GetRealisation<DependentClass>().First);
        dependentClass.First.Should().Be(dependentClass.Second);
    }
    
    [Fact]
    public void TestResolveTransient()
    {
        _diFactory.AddTransient<IGetGuidService, GetGuidService>();
        _diFactory.AddSingleton<DependentClass>();

        var dependentClass = _diFactory.GetRealisation<DependentClass>();

        dependentClass.First.Should().NotBe(dependentClass.Second);
        dependentClass.First.Should().Be(dependentClass.First);
        dependentClass.First.Should().Be(_diFactory.GetRealisation<DependentClass>().First);
    }
    
    [Fact]
    public void TestResolveTransientAll()
    {
        _diFactory.AddTransient<IGetGuidService, GetGuidService>();
        _diFactory.AddTransient<DependentClass>();

        var dependentClass = _diFactory.GetRealisation<DependentClass>();

        dependentClass.First.Should().NotBe(dependentClass.Second);
        dependentClass.First.Should().Be(dependentClass.First);
        dependentClass.First.Should().NotBe(_diFactory.GetRealisation<DependentClass>().First);
    }

    [Fact]
    public void TestResolveError()
    {
        _diFactory.AddSingleton<DependentClass>();

        Action getUnresolvedClass = () => _diFactory.GetRealisation<DependentClass>();

        getUnresolvedClass.Should().Throw<Exception>();
    }
}