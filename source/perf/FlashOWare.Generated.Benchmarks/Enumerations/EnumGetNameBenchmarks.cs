namespace FlashOWare.Benchmarks.Enumerations;

[ShortRunJob]
[MemoryDiagnoser(false)]
public class EnumGetNameBenchmarks
{
	[Benchmark]
	public string? Enum_GetName()
	{
		return SystemEnum.GetName(StringComparison.OrdinalIgnoreCase);
	}

	[Benchmark]
	public string? Enum_GetName_Generated()
	{
		return GeneratedEnum.GetName(StringComparison.OrdinalIgnoreCase);
	}

#if NET5_0_OR_GREATER
	[Benchmark]
	public string? Enum_GetName_Intercepted()
	{
		return Enum.GetName(StringComparison.OrdinalIgnoreCase);
	}
#endif
}
