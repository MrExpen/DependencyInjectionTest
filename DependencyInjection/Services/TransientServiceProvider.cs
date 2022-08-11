namespace DependencyInjection.Services;

public class TransientServiceProvider<T> : AbstractServiceProvider<T>
{
    public TransientServiceProvider(Func<IDIFactory, T>? providerFunction) : base(providerFunction)
    {
    }

    public override T GetService(IDIFactory dependencyProvider)
        => _providerFunction(dependencyProvider);

    public override LifeTime LifeTime => LifeTime.Transient;
}
