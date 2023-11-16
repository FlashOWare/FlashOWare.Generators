using System.Collections.Immutable;
using System.Diagnostics;

namespace FlashOWare.Generators.Enumerations;

internal sealed class EnumerationAttributeTarget(string? @namespace, string name, ImmutableArray<EnumerationTypeData> methods) : IEquatable<EnumerationAttributeTarget>
{
	internal string? Namespace { get; } = @namespace;
	internal string Name { get; } = name;
	internal ImmutableArray<EnumerationTypeData> Methods { get; } = methods;

	internal bool IsGlobalNamespace { get; } = @namespace is null;

	internal string GetFullName() => IsGlobalNamespace ? Name : $"{Namespace}.{Name}";

	public bool Equals(EnumerationAttributeTarget? other)
	{
		if (other is null)
		{
			return false;
		}
		else if (ReferenceEquals(this, other))
		{
			return true;
		}

		return Namespace == other.Namespace &&
			Name == other.Name &&
			Methods.SequenceEqual(other.Methods);
	}

	public override bool Equals(object? obj) => Equals(obj as EnumerationAttributeTarget);

	public override int GetHashCode() => throw new UnreachableException();
}
