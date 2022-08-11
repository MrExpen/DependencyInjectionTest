namespace DependencyInjection;

public interface IDIFactory
{
    #region Add

    #region Transient

    void AddTransient<T, TRealisation>(Func<IDIFactory, TRealisation>? func = null) where TRealisation : T;

    #region Extensions

    public void AddTransient<T>(Func<IDIFactory, T>? func = null)
        => AddTransient<T, T>(func);
    
    public void AddTransient<T>(Func<T>? func)
        => AddTransient<T, T>(func is null ? null : _ => func());

    public void AddTransient<T, TRealisation>(Func<TRealisation> func)
        where TRealisation : T
        => AddTransient<T, TRealisation>(_ => func());

    #endregion

    #endregion

    #region Singleton

    void AddSingleton<T, TRealisation>(Func<IDIFactory, TRealisation>? func = null) where TRealisation : T;

    #region Extensions

    public void AddSingleton<T>(Func<IDIFactory, T>? func = null)
        => AddSingleton<T, T>(func);
    
    public void AddSingleton<T>(Func<T>? func)
        => AddSingleton<T, T>(func is null ? null : _ => func());

    public void AddSingleton<T, TRealisation>(Func<TRealisation> func)
        where TRealisation : T
        => AddSingleton<T, TRealisation>(_ => func());

    #endregion

    #endregion

    #endregion

    #region Get

    object GetRealisation(Type type);
    
    public T GetRealisation<T>() => (T)GetRealisation(typeof(T));

    #endregion

    #region Contains

    bool ContainsRealisation(Type type);
    
    public bool ContainsRealisation<T>() => ContainsRealisation(typeof(T));

    #endregion

    #region Remove

    bool RemoveRealisation(Type type);
    
    public bool RemoveRealisation<T>() => RemoveRealisation(typeof(T));
    
    void ClearAll();

    #endregion

}