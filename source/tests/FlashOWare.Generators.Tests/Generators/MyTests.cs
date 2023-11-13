using FlashOWare.Generators;

namespace FlashOWare.Tests.Generators;

public class MyTests
{
	[Fact]
	public void Test_Generators()
	{
		Assert.NotNull(new MyGenerator());
	}
}
