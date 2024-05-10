namespace System;

#if !NET5_0_OR_GREATER
internal static class TypeExtensions
{
	public static bool IsAssignableTo(this Type @this, [NotNullWhen(true)] Type? targetType)
		=> targetType?.IsAssignableFrom(@this) ?? false;
}
#endif
