# GeneratedEnumIsDefinedAttribute<TEnum> Class
Attribute: [GeneratedEnumIsDefinedAttribute.cs](../../source/lib/FlashOWare.Generators.Attributes/Generators/GeneratedEnumIsDefinedAttribute.cs)

|            |                                    |
|------------|------------------------------------|
| Namespace  | `FlashOWare.Generators`            |
| Assembly   | `FlashOWare.Generators.Attributes` |
| Package    | `FlashOWare.Generators`            |
| Applies to | `[1.0.0,)`                         |

## Definition
```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class GeneratedEnumIsDefinedAttribute<TEnum> : Attribute
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
Commands the [System.Enum.IsDefined Generator](../gen/System.Enum.IsDefined-Generator.md) to generate a specialized implementation of [System.Enum.IsDefined<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.isdefined) for the specified `enum` type argument.

## Remarks
The _class_ this attribute is applied to:
- Must be a `partial` _class_.
- Must be a top_level _class_ (not a nested type).
- Must not be generic.
- Must not be a file-local type.

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
internal static partial class GeneratedEnum
{
}
```

## See also
- [Attribute Class](https://learn.microsoft.com/dotnet/api/system.attribute)
- [System.Enum.GetName<TEnum>(TEnum)](https://learn.microsoft.com/dotnet/api/system.enum.getname)

## History
- [vNext](../CHANGELOG.md#vNext)
