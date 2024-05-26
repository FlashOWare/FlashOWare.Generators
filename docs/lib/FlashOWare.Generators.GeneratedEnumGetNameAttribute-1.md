# GeneratedEnumGetNameAttribute<TEnum> Class
Attribute: [GeneratedEnumGetNameAttribute.cs](../../source/lib/FlashOWare.Generators.Attributes/Generators/GeneratedEnumGetNameAttribute.cs)

|            |                                    |
|------------|------------------------------------|
| Namespace  | `FlashOWare.Generators`            |
| Assembly   | `FlashOWare.Generators.Attributes` |
| Package    | `FlashOWare.Generators`            |
| Applies to | `[1.0.0,)`                         |

## Definition
```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class GeneratedEnumGetNameAttribute<TEnum> : Attribute
    where TEnum : struct, Enum
{
}
```

Inheritance: [Object](https://learn.microsoft.com/dotnet/api/system.object) -> [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute)

Attributes: [AttributeUsageAttribute](https://learn.microsoft.com/dotnet/api/system.attributeusageattribute)

### Type Parameters
- `TEnum`\
  The type of the enumeration.

## Summary
Commands the [System.Enum.GetName Generator](../gen/System.Enum.GetName-Generator.md) to generate a specialized implementation of [System.Enum.GetName<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.getname) for the specified `enum` type argument.

## Remarks
The _class_ this attribute is applied to:
- Must be a `partial` _class_.
- Must be a top-level _class_ (not a nested type).
- Must not be generic.
- Must not be a file-local type.

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
internal static partial class GeneratedEnum;
```

## See also
- [Attribute Class](https://learn.microsoft.com/dotnet/api/system.attribute)
- [System.Enum.GetName<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.getname)

## History
- [vNext](../CHANGELOG.md#vNext)
