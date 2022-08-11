namespace DependencyInjection.Services.Factory;

public class ServiceProviderFactory : IServiceProviderFactory
{
    public IServiceProvider<T> GetServiceProvider<T>(LifeTime lifeTime, Func<IDIFactory, T>? func)
    {
        return lifeTime switch
        {
            LifeTime.Singleton => new SingletonServiceProvider<T>(func),
            LifeTime.Transient => new TransientServiceProvider<T>(func),
            _ => throw new ArgumentOutOfRangeException(nameof(lifeTime))
        };
    }

    public IServiceProvider<T> GetSingletonServiceProvider<T>(T instance) => new SingletonServiceProvider<T>(instance);
}