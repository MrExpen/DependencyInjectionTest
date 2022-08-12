using DependencyInjection.Exceptions;
using DependencyInjection.UnitTests.Fake;
using FluentAssertions;

namespace DependencyInjection.UnitTests;

public class AddGetTests
{
    private readonly IDIFactory _diFactory;

    public AddGetTests()
    {
        _diFactory = new DIFactory();
    }
    
    [Fact]
    public void TestSingleton()
    {
        _diFactory.AddSingleton<IGetGuidService, GetGuidService>();
        _diFactory.AddSingleton<GetGuidService>();

        _diFactory.GetRealisation<IGetGuidService>().Guid.Should()
            .Be(_diFactory.GetRealisation<IGetGuidService>().Guid);
        
        _diFactory.GetRealisation<GetGuidService>().Guid.Should()
            .Be(_diFactory.GetRealisation<GetGuidService>().Guid);
        
        _diFactory.GetRealisation<IGetGuidService>().Guid.Should()
            .NotBe(_diFactory.GetRealisation<GetGuidService>().Guid);
    }
    
    [Fact]
    public void TestTransient()
    {
        _diFactory.AddTransient<IGetGuidService, GetGuidService>();
        _diFactory.AddTransient<GetGuidService>();

        _diFactory.GetRealisation<IGetGuidService>().Guid.Should()
            .NotBe(_diFactory.GetRealisation<IGetGuidService>().Guid);
        
        _diFactory.GetRealisation<GetGuidService>().Guid.Should()
            .NotBe(_diFactory.GetRealisation<GetGuidService>().Guid);
        
        _diFactory.GetRealisation<IGetGuidService>().Guid.Should()
            .NotBe(_diFactory.GetRealisation<GetGuidService>().Guid);
    }

    [Fact]
    public void TestAddAlreadyAdded()
    {
        Action addService = () => _diFactory.AddSingleton<GetGuidService>();
        addService.Should().NotThrow();
        addService.Should().Throw<TypeAlreadyRegisteredException>();
    }
}