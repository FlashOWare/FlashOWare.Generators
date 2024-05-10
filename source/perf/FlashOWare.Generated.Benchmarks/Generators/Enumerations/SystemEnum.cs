namespace FlashOWare.Benchmarks.Generators.Enumerations;

internal static class SystemEnum
{
	internal static string? GetName<TEnum>(TEnum value) where TEnum : struct, Enum
	{
#if NET5_0_OR_GREATER
		return Enum.GetName(value);
#else
		return Enum.GetName(typeof(TEnum), value);
#endif
	}

	internal static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum
	{
#if NET5_0_OR_GREATER
		return Enum.IsDefined(value);
#else
		return Enum.IsDefined(typeof(TEnum), value);
#endif
	}
}
