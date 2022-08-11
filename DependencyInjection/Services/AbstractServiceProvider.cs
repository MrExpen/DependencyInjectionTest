namespace DependencyInjection.Services;

public abstract class AbstractServiceProvider<T> : IServiceProvider<T>
{
    public Type Type => typeof(T);
    object IServiceProvider.GetService(IDIFactory dependencyProvider) => GetService(dependencyProvider);

    public abstract T GetService(IDIFactory dependencyProvider);

    public abstract LifeTime LifeTime { get; }
    
    protected readonly Func<IDIFactory, T> _providerFunction;

    protected AbstractServiceProvider(Func<IDIFactory, T>? providerFunction)
    {
        _providerFunction = providerFunction ?? _defaultFunctionProvider;
    }

    protected static T _defaultFunctionProvider(IDIFactory dependencyProvider)
    {
        foreach (var constructorInfo in typeof(T).GetConstructors().Where(x => x.IsPublic))
        {
            var parametersInfo = constructorInfo.GetParameters();
            if (!parametersInfo.All(x => dependencyProvider.ContainsRealisation(x.ParameterType))) continue;
            
            var args = new object?[parametersInfo.Length];

            for (var i = 0; i < parametersInfo.Length; i++)
            {
                args[i] = dependencyProvider.GetRealisation(parametersInfo[i].ParameterType);
            }

            return (T)Activator.CreateInstance(typeof(T), args)!;
        }

        throw new Exception($"Can not resolve class {typeof(T)}");
    }
}