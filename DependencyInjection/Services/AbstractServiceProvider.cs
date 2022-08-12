using System.Reflection;
using DependencyInjection.Exceptions;

namespace DependencyInjection.Services;

public abstract class AbstractServiceProvider<T> : IServiceProvider<T>
{
    public Type Type => typeof(T);
    object IServiceProvider.GetService(IDIFactory dependencyProvider) => GetService(dependencyProvider)!;

    public abstract T GetService(IDIFactory dependencyProvider);

    public abstract LifeTime LifeTime { get; }
    
    protected readonly Func<IDIFactory, T> ProviderFunction;

    protected AbstractServiceProvider(Func<IDIFactory, T>? providerFunction)
    {
        ProviderFunction = providerFunction ?? _defaultFunctionProvider;
    }

    protected static T _defaultFunctionProvider(IDIFactory dependencyProvider)
    {
        if (dependencyProvider.CanResolveFor<T>())
        {
            var constructorInfo = dependencyProvider.GetResolvedConstructor<T>();
            var parametersInfo = constructorInfo.GetParameters();
            
            var args = new object?[parametersInfo.Length];

            for (var i = 0; i < parametersInfo.Length; i++)
            {
                if (dependencyProvider.ContainsRealisation(parametersInfo[i].ParameterType))
                {
                    args[i] = dependencyProvider.GetRealisation(parametersInfo[i].ParameterType);
                }
                else if (parametersInfo[i].HasDefaultValue)
                {
                    args[i] = parametersInfo[i].DefaultValue;
                }
                else
                {
                    if (parametersInfo[i].ParameterType.GetConstructors()
                        .Any(x => x.IsPublic && x.GetParameters().Length == 0))
                    {
                        args[i] = Activator.CreateInstance(parametersInfo[i].ParameterType);
                    }
                    else
                    {
                        throw new CannotResolveAnyConstructorException();
                    }
                }
            }

            return (T)Activator.CreateInstance(typeof(T), args)!;
        }
        
        throw new CannotResolveAnyConstructorException($"Can not resolve class {typeof(T)}");
    }
}