# System.Enum.IsDefined Generator
Generator: [EnumIsDefinedGenerator.cs](../../source/gen/FlashOWare.Generators/Generators/Enumerations/EnumIsDefinedGenerator.cs)

|                  |                                 |
|------------------|---------------------------------|
| HintName         | `{Target-Class}.IsDefined.g.cs` |
| Language         | C# 11.0 or greater              |
| Target Framework | .NET Standard 1.0 or compatible |
| Dependencies     | _none_                          |
| Applies to       | `[1.0.0,)`                      |

## Summary
Generates specialized variants of the [System.Enum.IsDefined<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.isdefined) method, per `enum` type, with linear search characteristics.

## Remarks
This Generator emits non-generic `static bool IsDefined(TEnum)` methods on `partial` classes annotated with the generic [GeneratedEnumIsDefinedAttribute<TEnum>](../lib/FlashOWare.Generators.GeneratedEnumIsDefinedAttribute-1.md) _Attribute_.

The generated methods return a `bool` telling whether a given integral value, or its name, exists in a specified enumeration.

(Mirrors the behavior of [System.Enum.IsDefined<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.isdefined))

## Example
```csharp
using System;
using FlashOWare.Generators;

_ = GeneratedEnum.IsDefined(ConsoleKey.F); //true
_ = GeneratedEnum.IsDefined(ConsoleKey.NumPad0); //true
_ = GeneratedEnum.IsDefined((TypeCode)18); //true
_ = GeneratedEnum.IsDefined((TypeCode)Int32.MaxValue); //false
_ = GeneratedEnum.IsDefined(AttributeTargets.All); //true
_ = GeneratedEnum.IsDefined(AttributeTargets.Struct | AttributeTargets.Enum); //false

[GeneratedEnumIsDefinedAttribute<ConsoleKey>]
[GeneratedEnumIsDefinedAttribute<TypeCode>]
[GeneratedEnumIsDefinedAttribute<AttributeTargets>]
internal static partial class GeneratedEnum;
```

## See also
- [Enumeration types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/enum)
- [Implementation Details Matter, by David Fowler, at iO .NET Virtual Meetup](https://www.youtube.com/watch?v=Cmh5wxM1NkI&t=3150s)
- [Implementation Details Matter, by David Fowler, at Dotnetos Conference 2021](https://www.youtube.com/watch?v=Uyg4_4TZINE&t=2117s)
- [C#'s Enum performance trap your code is suffering from, by Nick Chapsas](https://www.youtube.com/watch?v=BoE5Y6Xkm6w)
- [Enums in .NET 8 Are FAST, but Mine Are Faster!, by Nick Chapsas](https://www.youtube.com/watch?v=UBY4Y6AykdM)

## History
- [vNext](../CHANGELOG.md#vNext)
