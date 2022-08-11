namespace DependencyInjection;

public interface IDIFactory
{
    #region Add

    void AddTransient<T, TRealisation>(Func<IDIFactory, TRealisation>? func = null) where TRealisation : T;
    
    void AddSingleton<T, TRealisation>(Func<IDIFactory, TRealisation>? func = null) where TRealisation : T;
    
    #endregion

    #region Get

    object GetRealisation(Type type);

    #endregion

    #region Contains

    bool ContainsRealisation(Type type);

    #endregion

    #region Remove

    bool RemoveRealisation(Type type);
    void ClearAll();

    #endregion
}