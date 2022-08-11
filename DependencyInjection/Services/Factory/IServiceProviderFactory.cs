﻿namespace DependencyInjection.Services.Factory;

public interface IServiceProviderFactory
{
    IServiceProvider<T> GetServiceProvider<T>(LifeTime lifeTime, Func<IDIFactory, T>? func);
}