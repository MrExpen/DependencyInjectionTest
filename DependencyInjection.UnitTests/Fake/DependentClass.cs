namespace DependencyInjection.UnitTests.Fake;

public class DependentClass
{
    private readonly IGetGuidService _firstsService;
    private readonly IGetGuidService _secondService;

    public DependentClass(IGetGuidService firstsService, IGetGuidService secondService)
    {
        _firstsService = firstsService;
        _secondService = secondService;
    }

    public Guid First => _firstsService.Guid;
    public Guid Second => _secondService.Guid;
}