namespace FlashOWare.Generators;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class GeneratedEnumGetNameAttribute<TEnum> : Attribute
	where TEnum : struct, Enum
{
}
