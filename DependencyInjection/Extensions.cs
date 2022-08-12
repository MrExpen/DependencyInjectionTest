using System.Reflection;

namespace DependencyInjection;

public static class Extensions
{
    public static bool CanResolveFor(this IDIFactory factory, Type type)
    {
        return type.GetConstructors()
            .Where(x => x.IsPublic)
            .OrderBy(c => c.GetParameters().Length)
            .Select(constructorInfo => constructorInfo.GetParameters())
            .Any(parametersInfo => 
                parametersInfo.All(
                    parameter => parameter.HasDefaultValue 
                                 || factory.ContainsRealisation(parameter.ParameterType) 
                                 || parameter.ParameterType.GetConstructors()
                                     .Any(c => c.IsPublic && c.GetParameters().Length == 0) 
                                 || factory.CanResolveFor(parameter.ParameterType)
                                 )
                );
    }

    public static bool CanResolveFor<T>(this IDIFactory factory) => factory.CanResolveFor(typeof(T));

    internal static ConstructorInfo GetResolvedConstructor(this IDIFactory factory, Type type)
    {
        foreach (var constructorInfo in type.GetConstructors()
                     .Where(x => x.IsPublic)
                     .OrderBy(c => c.GetParameters().Length))
        {
            if (constructorInfo.GetParameters()
                .All(
                    parameter => parameter.HasDefaultValue 
                                 || factory.ContainsRealisation(parameter.ParameterType) 
                                 || parameter.ParameterType.GetConstructors()
                                     .Any(c => c.IsPublic && c.GetParameters().Length == 0) 
                                 || factory.CanResolveFor(parameter.ParameterType)
                    ))
            {
                return constructorInfo;
            }
        }

        throw new Exception();
    }

    internal static ConstructorInfo GetResolvedConstructor<T>(this IDIFactory factory)
        => factory.GetResolvedConstructor(typeof(T));
}
