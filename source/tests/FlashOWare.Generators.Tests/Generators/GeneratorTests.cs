using FlashOWare.Generators.Enumerations;
using Microsoft.CodeAnalysis;

namespace FlashOWare.Tests.Generators;

public class GeneratorTests
{
	[Fact]
	public void All_Generators()
	{
		Type[] generators = typeof(EnumInterceptorGenerator).Assembly.GetTypes()
			.Where(static (Type type) => type.IsAssignableTo(typeof(IIncrementalGenerator)))
			.ToArray();

		Assert.Equal(3, generators.Length);
		Assert.All(generators, static (Type type) => Assert.Equal([typeof(IIncrementalGenerator)], type.GetInterfaces()));
		Assert.All(generators, static (Type type) => Assert.Equal(typeof(GeneratorAttribute), type.CustomAttributes.Single().AttributeType));
	}
}
