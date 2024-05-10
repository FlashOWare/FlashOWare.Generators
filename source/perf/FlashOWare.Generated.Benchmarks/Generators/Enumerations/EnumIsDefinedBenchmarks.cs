namespace FlashOWare.Benchmarks.Enumerations;

[ShortRunJob]
[MemoryDiagnoser(false)]
public class EnumIsDefinedBenchmarks
{
	[Benchmark]
	public bool Enum_IsDefined()
	{
		return SystemEnum.IsDefined(StringComparison.OrdinalIgnoreCase);
	}

	[Benchmark]
	public bool Enum_IsDefined_Generated()
	{
		return GeneratedEnum.IsDefined(StringComparison.OrdinalIgnoreCase);
	}

#if NET5_0_OR_GREATER
	[Benchmark]
	public bool Enum_IsDefined_Intercepted()
	{
		return Enum.IsDefined(StringComparison.OrdinalIgnoreCase);
	}
#endif
}
