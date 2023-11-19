# System.Enum.GetName Generator
Generator: [EnumGetNameGenerator.cs](../../source/gen/FlashOWare.Generators/Generators/Enumerations/EnumGetNameGenerator.cs)

|                  |                                 |
|------------------|---------------------------------|
| HintName         | `{Target-Class}.GetName.g.cs`   |
| Language         | C# 11.0 or greater              |
| Target Framework | .NET Standard 1.0 or compatible |
| Dependencies     | _none_                          |
| Applies to       | `[1.0.0,)`                      |

## Summary
Generates specialized variants of the [System.Enum.GetName<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.getname) method, per `enum` type, with linear search characteristics.

## Remarks
This Generator emits non-generic `static string? GetName(TEnum)` methods on `partial` classes annotated with the generic [GeneratedEnumGetNameAttribute<TEnum>](../lib/FlashOWare.Generators.GeneratedEnumGetNameAttribute-1.md) _Attribute_.

The generated methods return a `string?` containing the name of the enumerated constant of its underlying enumeration type; or `null` if no such constant is found.

(Mirrors the behavior of [System.Enum.GetName<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.getname))

## Example
```csharp
using System;
using FlashOWare.Generators;

_ = GeneratedEnum.GetName(ConsoleKey.F); //F
_ = GeneratedEnum.GetName(ConsoleKey.NumPad0); //NumPad0
_ = GeneratedEnum.GetName((TypeCode)18); //String
_ = GeneratedEnum.GetName((TypeCode)Int32.MaxValue); //<null>
_ = GeneratedEnum.GetName(AttributeTargets.All); //All
_ = GeneratedEnum.GetName(AttributeTargets.Struct | AttributeTargets.Enum); //<null>

[GeneratedEnumGetName<ConsoleKey>]
[GeneratedEnumGetName<TypeCode>]
[GeneratedEnumGetName<AttributeTargets>]
internal static partial class GeneratedEnum
{
}
```

## See also
- [Enumeration types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/enum)
- [Implementation Details Matter, by David Fowler, at iO .NET Virtual Meetup](https://www.youtube.com/watch?v=Cmh5wxM1NkI&t=3150s)
- [Implementation Details Matter, by David Fowler, at Dotnetos Conference 2021](https://www.youtube.com/watch?v=Uyg4_4TZINE&t=2117s)
- [C#'s Enum performance trap your code is suffering from, by Nick Chapsas](https://www.youtube.com/watch?v=BoE5Y6Xkm6w)
- [Enums in .NET 8 Are FAST, but Mine Are Faster!, by Nick Chapsas](https://www.youtube.com/watch?v=UBY4Y6AykdM)

## History
- [vNext](../CHANGELOG.md#vNext)
