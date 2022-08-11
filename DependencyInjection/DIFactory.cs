using DependencyInjection.Services.Factory;
using IServiceProvider = DependencyInjection.Services.IServiceProvider;

namespace DependencyInjection;

public class DIFactory : IDIFactory
{
    private readonly Dictionary<Type, IServiceProvider> _dependencies = new();
    private readonly object _lock = new();
    private readonly IServiceProviderFactory _serviceProviderFactory = new ServiceProviderFactory();

    public void AddTransient<T, TRealisation>(Func<IDIFactory, TRealisation>? func = null) where TRealisation : T
        => AddService<T, TRealisation>(LifeTime.Transient, func);

    public void AddSingleton<T, TRealisation>(Func<IDIFactory, TRealisation>? func = null) where TRealisation : T
        => AddService<T, TRealisation>(LifeTime.Singleton, func);

    private void AddService<T, TRealisation>(LifeTime lifeTime, Func<IDIFactory, TRealisation>? func)
    {
        EnsureNotPresent<T>();

        lock (_lock)
        {
            _dependencies[typeof(T)] = _serviceProviderFactory.GetServiceProvider(lifeTime, func);
        }
    }

    public object GetRealisation(Type type)
    {
        lock (_lock)
        {
            return _dependencies[type].GetService(this);
        }
    }

    public bool ContainsRealisation(Type type)
    {
        lock (_lock)
        {
            return _dependencies.ContainsKey(type);
        }
    }

    public bool RemoveRealisation(Type type)
    {
        lock (_lock)
        {
            return _dependencies.Remove(type);
        }
    }

    public void ClearAll()
    {
        lock (_lock)
        {
            _dependencies.Clear();
        }
    }

    private void EnsureNotPresent<T>()
    {
        if (ContainsRealisation(typeof(T)))
            throw new Exception($"Realisation for type ${typeof(T)} already exists.");
    }
}