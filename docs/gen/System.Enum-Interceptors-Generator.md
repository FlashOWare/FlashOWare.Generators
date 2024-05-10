# System.Enum Interceptor Generator
Generator: [EnumInterceptorGenerator.cs](../../source/gen/FlashOWare.Generators/Generators/Enumerations/EnumInterceptorGenerator.cs)

|                  |                                              |
|------------------|----------------------------------------------|
| HintName         | `FlashOWare.Generated.EnumInterceptors.g.cs` |
| Language         | C# 11.0 or greater                           |
| Target Framework | .NET Standard 1.0 or compatible              |
| Dependencies     | _none_                                       |
| Applies to       | `[1.0.0,)`                                   |

> [!WARNING]
> Interceptors are an experimental feature
> - Available in preview mode with the _.NET 7.0.400 SDK_ (or greater)
>   - requires `<Features>InterceptorsPreview</Features>`
> - Available in preview mode with the _.NET 8.0.100 SDK_ (or greater)
>   - requires `<InterceptorsPreviewNamespaces>$(InterceptorsPreviewNamespaces);My.Generated.Namespace</InterceptorsPreviewNamespaces>`

## Summary
Generates experimental _Interceptors_ for _static_ [System.Enum](https://learn.microsoft.com/dotnet/api/system.enum) method invocations.

## Remarks
This Generator emits specialized variants as experimental _Interceptors_, per `enum` type, with linear search characteristics, for
- for [System.Enum.GetName<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.getname) method invocations
- for [System.Enum.IsDefined<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.isdefined) method invocations

## Example
```csharp
using System;

_ = Enum.GetName(ConsoleKey.F); //F
_ = Enum.IsDefined(ConsoleKey.NumPad0); //true
```

## See also
- [Enumeration types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/enum)
- [Interceptors: Proposal](https://github.com/dotnet/csharplang/issues/7009)
- [Interceptors: Feature Specification](https://github.com/dotnet/roslyn/blob/main/docs/features/interceptors.md)
- [Interceptors: Languages & Runtime Community Standup](https://www.youtube.com/watch?v=X1_QeH1yAto)
- [Experimental C# Interceptors: AOT & Performance for free | .NET Conf 2023](https://www.youtube.com/watch?v=wadfRRrEOh4&list=PLdo4fOcmZ0oULyHSPBx-tQzePOYlhvrAU&index=49)

## History
- [vNext](../CHANGELOG.md#vNext)
