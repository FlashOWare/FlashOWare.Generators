# FlashOWare.Generators
Prerelease Changelog

[goto Release_Changelog;](./CHANGELOG.md)

## [vNext]
### Generators
- **Changed** `Enum-Interceptor-Generator`
  - requires _C# 12.0_ --> _C# 11.0_
    - emits _regular constructors_ instead of _primary constructors_ for the (experimental) interceptors Attribute
    - consolidates required language version across all Generators (_C# 11.0_)
    - aligns with required minimum _.NET 7.0 SDK_, where the _TFM_ `net7.0` uses `11.0` as default `LangVersion`

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
