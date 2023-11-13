using FlashOWare.Generators;

namespace FlashOWare.Benchmarks.Generated;

public class MyBenchmarks
{
	[Benchmark]
	public object RunGenerated()
	{
		return new MyAttribute();
	}
}
