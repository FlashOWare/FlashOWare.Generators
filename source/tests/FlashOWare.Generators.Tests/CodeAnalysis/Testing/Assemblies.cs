using System.Reflection;
using FlashOWare.Generators;

namespace FlashOWare.Tests.CodeAnalysis.Testing;

internal static class Assemblies
{
	public static Assembly Attributes { get; } = GetAttributesAssembly();

	private static Assembly GetAttributesAssembly()
	{
		IEnumerable<Assembly> references = EnumerateAttributeTypes()
			.Select(static (Type type) => type.Assembly)
			.Distinct();

		Assembly reference = references.Single();

		return reference;
	}

	private static IEnumerable<Type> EnumerateAttributeTypes()
	{
		yield return typeof(GeneratedEnumGetNameAttribute<>);
		yield return typeof(GeneratedEnumIsDefinedAttribute<>);
	}
}
