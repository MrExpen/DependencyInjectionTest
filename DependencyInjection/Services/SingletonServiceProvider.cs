namespace DependencyInjection.Services;

public class SingletonServiceProvider<T> : AbstractServiceProvider<T>
{
    private T? _cache;
    public override T GetService(IDIFactory dependencyProvider)
    {
        if (_cache is null)
            _cache = _providerFunction(dependencyProvider);

        return _cache;
    }

    public override LifeTime LifeTime => LifeTime.Singleton;

    public SingletonServiceProvider(Func<IDIFactory, T>? providerFunction) : base(providerFunction)
    {
    }

    public SingletonServiceProvider(T instance) : base(null)
    {
        _cache = instance;
    }
}