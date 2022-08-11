# Dependency Injection Project

In software engineering, **dependency injection** is a **design pattern** in which an object or function receives other objects or functions that it depends on. A form of inversion of control, dependency injection aims to separate the concerns of constructing objects and using them, leading to loosely coupled programs.

## Provided Functional:
* AddServices of type
* Resolve constructors of dependent class if dependence is registered in factory
* Singleton and Transient Service life times

## Examples:

### Singleton
###### Singleton - same object every time you call a fabric method.
```csharp
var diFactory = new DIFactory();
diFactory.AddSingleton<IGetGuidService, GetGuidService>();

var service = diFactory.GetRealisation<IGetGuidService>();
```
```csharp
service == diFactory.GetRealisation<IGetGuidService>();
```

### Transient
###### Transient - ~~same~~ new object every time you call a fabric method.
```csharp
var diFactory = new DIFactory();
diFactory.AddTransient<IGetGuidService, GetGuidService>();

var service = diFactory.GetRealisation<IGetGuidService>();
```
```csharp
service != diFactory.GetRealisation<IGetGuidService>();
```