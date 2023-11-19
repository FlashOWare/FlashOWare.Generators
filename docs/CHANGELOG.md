# FlashOWare.Generators
Release Changelog

[goto Prerelease_Changelog;](./CHANGELOG-Prerelease.md)

## [vNext]
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

[vnext]: https://github.com/FlashOWare/FlashOWare.Generators/commits/main
