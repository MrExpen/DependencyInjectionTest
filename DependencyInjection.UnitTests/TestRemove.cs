using DependencyInjection.UnitTests.Fake;
using FluentAssertions;

namespace DependencyInjection.UnitTests;

public class TestRemove
{
    private readonly IDIFactory _diFactory;

    public TestRemove()
    {
        _diFactory = new DIFactory();
        
        _diFactory.AddSingleton<IGetGuidService>();
        _diFactory.AddTransient<GetGuidService>();
    }

    [Fact]
    public void TestRemoveType()
    {
        _diFactory.RemoveRealisation<IGetGuidService>();

        _diFactory.GetRealisation<GetGuidService>();

        Action getRemovedService = () => _diFactory.GetRealisation<IGetGuidService>();

        getRemovedService.Should().Throw<KeyNotFoundException>();
    }
    
    [Fact]
    public void TestClear()
    {
        _diFactory.ClearAll();

        Action getRemovedService1 = () => _diFactory.GetRealisation<GetGuidService>();

        Action getRemovedService2 = () => _diFactory.GetRealisation<IGetGuidService>();

        getRemovedService1.Should().Throw<KeyNotFoundException>();
        getRemovedService2.Should().Throw<KeyNotFoundException>();
    }
}