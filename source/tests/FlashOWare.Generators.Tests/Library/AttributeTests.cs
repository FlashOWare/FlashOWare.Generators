using System.Reflection;
using FlashOWare.Generators;

namespace FlashOWare.Tests.Library;

public class AttributeTests
{
	[Theory]
	[InlineData(typeof(GeneratedEnumGetNameAttribute<>))]
	[InlineData(typeof(GeneratedEnumIsDefinedAttribute<>))]
	public void All_Attributes(Type type)
	{
		CustomAttributeData data = Assert.Single(type.CustomAttributes);

		Assert.Equal(typeof(AttributeUsageAttribute), data.AttributeType);
		Assert.Collection(data.ConstructorArguments,
			static (CustomAttributeTypedArgument element) =>
			{
				Assert.Equal(typeof(AttributeTargets), element.ArgumentType);
				Assert.Equal((int)AttributeTargets.Class, element.Value);
			});
		Assert.Collection(data.NamedArguments.OrderBy(static (CustomAttributeNamedArgument arg) => arg.MemberName),
			static (CustomAttributeNamedArgument element) =>
			{
				Assert.Equal(typeof(bool), element.TypedValue.ArgumentType);
				Assert.Equal(true, element.TypedValue.Value);
				Assert.Equal(nameof(AttributeUsageAttribute.AllowMultiple), element.MemberName);
				Assert.False(element.IsField);
			},
			static (CustomAttributeNamedArgument element) =>
			{
				Assert.Equal(typeof(bool), element.TypedValue.ArgumentType);
				Assert.Equal(false, element.TypedValue.Value);
				Assert.Equal(nameof(AttributeUsageAttribute.Inherited), element.MemberName);
				Assert.False(element.IsField);
			});
	}
}
