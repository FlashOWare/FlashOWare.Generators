using FlashOWare.Generators.Enumerations;
using Microsoft.CodeAnalysis;

namespace FlashOWare.Tests.Enumerations;

public class GeneratorTests
{
	[Theory]
	[InlineData(typeof(EnumGetNameGenerator))]
	[InlineData(typeof(EnumIsDefinedGenerator))]
	[InlineData(typeof(EnumInterceptorGenerator))]
	public void Generators(Type type)
	{
		Assert.Multiple(
			() => Assert.Equal([typeof(IIncrementalGenerator)], type.GetInterfaces()),
			() => Assert.Equal(typeof(GeneratorAttribute), type.CustomAttributes.Single().AttributeType)
		);
	}
}
