# FlashOWare.Generators
Prerelease Changelog

[goto Release_Changelog;](./CHANGELOG.md)

## [vNext]
### Generators
- **Changed** `Enum-Interceptor-Generator` to emit each `System.Enum` specialization only once, but with one or more Interceptor-Attributes instead.

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
