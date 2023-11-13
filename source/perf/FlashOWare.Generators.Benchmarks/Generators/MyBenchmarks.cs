using FlashOWare.Generators;

namespace FlashOWare.Benchmarks.Generators;

public class MyBenchmarks
{
	[Benchmark]
	public object RunGenerators()
	{
		return new MyGenerator();
	}
}
