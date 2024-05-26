# FlashOWare.Generators
Prerelease Changelog

[goto Release_Changelog;](./CHANGELOG.md)

## [vNext]
### Generators
- **Changed** `Enum.GetName-Generator` suppressing duplicate method declarations when another `GeneratedEnumGetNameAttribute` has the same _enum_ type argument
- **Changed** `Enum.IsDefined-Generator` suppressing duplicate method declarations when another `GeneratedEnumIsDefinedAttribute` has the same _enum_ type argument
- **Changed** `Enum-Interceptor-Generator` requirement from _C# 12.0_ to _C# 11.0_
  - emit a _regular constructor_ instead of a _primary constructor_ for the (experimental) Interceptors Attribute
  - consolidate required language version across all Generators (_C# 11.0_)
  - align with required minimum _.NET 7.0 SDK_, where the _TFM_ `net7.0` uses `11.0` as default `LangVersion`
- **Changed** `Enum-Interceptor-Generator` to suppress adding the generated document when no target Enum-Method-Invocations are found
- **Fixed** _InvalidOperationException_ in `Enum.GetName-Generator` when the type argument of `GeneratedEnumGetNameAttribute` is of _error_ kind
- **Fixed** _InvalidOperationException_ in `Enum.IsDefined-Generator` when the type argument of `GeneratedEnumIsDefinedAttribute` is of _error_ kind
- **Fixed** _ArgumentException_ in `Enum.GetName-Generator` when multiple _partial class type declarations_ have at least one `GeneratedEnumGetNameAttribute`
- **Fixed** _ArgumentException_ in `Enum.IsDefined-Generator` when multiple _partial class type declarations_ have at least one `GeneratedEnumIsDefinedAttribute`
- **Fixed** `Enum.GetName-Generator` generating invalid C# source when the type argument of `GeneratedEnumGetNameAttribute` is not an _enum_ type
- **Fixed** `Enum.IsDefined-Generator` generating invalid C# source when the type argument of `GeneratedEnumIsDefinedAttribute` is not an _enum_ type

### Package
- **Added** documentation to `README.md`.

## [1.0.0-prerelease.0] - 2023-11-16
### Generators
- **Added** `Enum.GetName-Generator` (_C#_ only)
  - generates a specialized `static string? GetName(TEnum)` method group
  - via `FlashOWare.Generators.GeneratedEnumGetNameAttribute<TEnum>`
- **Added** `Enum.IsDefined-Generator` (_C#_ only)
  - generates a specialized `static bool IsDefined(TEnum)` method group
  - via `FlashOWare.Generators.GeneratedEnumIsDefinedAttribute<TEnum>`
- **Added** `Enum-Interceptor-Generator` (_C#_ only)
  - generates experimental _Interceptors_
    - for `System.Enum.GetName<TEnum>(TEnum)` method invocations
    - for `System.Enum.IsDefined<TEnum>(TEnum)` method invocations

### Library
- **Added** generic _Attribute_ `FlashOWare.Generators.GeneratedEnumGetNameAttribute<TEnum>`
  - used by `Enum.GetName-Generator`
- **Added** generic _Attribute_ `FlashOWare.Generators.GeneratedEnumIsDefinedAttribute<TEnum>`
  - used by `Enum.IsDefined-Generator`

### Package
- **Added** `FlashOWare.Generators` (as _Analyzers_) targeting _.NET Standard 2.0_.
- **Added** `FlashOWare.Generators.Attributes` (as _Library_) targeting _.NET Standard 2.0_.
- **Added** `FlashOWare.Generators.props`
  - _empty_
- **Added** `FlashOWare.Generators.targets`
  - sets Property `InterceptorsPreviewNamespaces` to `$(InterceptorsPreviewNamespaces);FlashOWare.Generated`

[vnext]: https://github.com/FlashOWare/FlashOWare.Generators/compare/v1.0.0-prerelease.0...HEAD
[1.0.0-prerelease.0]: https://github.com/FlashOWare/FlashOWare.Generators/releases/tag/v1.0.0-prerelease.0
