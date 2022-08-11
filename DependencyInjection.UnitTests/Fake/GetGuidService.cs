namespace DependencyInjection.UnitTests.Fake;

public class GetGuidService : IGetGuidService
{
    public Guid Guid { get; } = Guid.NewGuid();
}