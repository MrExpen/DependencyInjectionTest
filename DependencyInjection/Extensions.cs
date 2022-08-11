namespace DependencyInjection;

public static class Extensions
{
    public static void AddTransient<T>(this IDIFactory factory, Func<IDIFactory, T>? func = null)
        => factory.AddTransient<T, T>(func);

    public static void AddSingleton<T>(this IDIFactory factory, Func<IDIFactory, T>? func = null)
        => factory.AddSingleton<T, T>(func);

    public static void AddTransient<T, TRealisation>(this IDIFactory factory, Func<TRealisation>? func = null)
        where TRealisation : T
        => factory.AddTransient<T, TRealisation>(func is null ? null : x => func());

    public static void AddSingleton<T, TRealisation>(this IDIFactory factory, Func<TRealisation>? func = null)
        where TRealisation : T
        => factory.AddSingleton<T, TRealisation>(func is null ? null : x => func());
    
    public static void AddTransient<T>(this IDIFactory factory, Func<T>? func)
        => factory.AddTransient<T, T>(func is null ? null : x => func());

    public static void AddSingleton<T>(this IDIFactory factory, Func<T>? func)
        => factory.AddSingleton<T, T>(func is null ? null : x => func());
    
    public static T GetRealisation<T>(this IDIFactory factory)
        => (T)factory.GetRealisation(typeof(T));

    public static bool ContainsRealisation<T>(this IDIFactory factory)
        => factory.ContainsRealisation(typeof(T));
    
    public static bool RemoveRealisation<T>(this IDIFactory factory)
        => factory.RemoveRealisation(typeof(T));
}