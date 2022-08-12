# Dependency Injection Project

In software engineering, **dependency injection** is a **design pattern** in which an object or function receives other objects or functions that it depends on. A form of inversion of control, dependency injection aims to separate the concerns of constructing objects and using them, leading to loosely coupled programs.

## Provided Functional:
* AddServices of type
* Resolve constructors of dependent class if dependence is registered in factory
* Singleton and Transient Service life times

## Peculiarities:
* Uses the constructor with the most parameters
* Resolved registered types
* While resolving registered types can:
  * Construct another registered types
  * Construct unregistered types with default constructor
  * Leave a default value of parameter if presents and only if can not resolve this type

## Examples:

### Singleton
Singleton - same object every time you call a fabric method.
```csharp
var diFactory = new DIFactory();
diFactory.AddSingleton<IGetGuidService, GetGuidService>();

var service = diFactory.GetRealisation<IGetGuidService>();
```
```csharp
service == diFactory.GetRealisation<IGetGuidService>();
```

### Transient
Transient - ~~same~~ **new** object every time you call a fabric method.
```csharp
var diFactory = new DIFactory();
diFactory.AddTransient<IGetGuidService, GetGuidService>();

var service = diFactory.GetRealisation<IGetGuidService>();
```
```csharp
service != diFactory.GetRealisation<IGetGuidService>();
```
