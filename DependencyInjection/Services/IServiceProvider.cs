namespace DependencyInjection.Services;

public interface IServiceProvider
{
    Type Type { get; }
    object GetService(IDIFactory dependencyProvider);
    LifeTime LifeTime { get; }
}

public interface IServiceProvider<out T> : IServiceProvider
{
    new T GetService(IDIFactory dependencyProvider);
}