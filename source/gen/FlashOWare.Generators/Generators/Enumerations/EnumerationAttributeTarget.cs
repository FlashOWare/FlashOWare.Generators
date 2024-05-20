using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

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

	public override string ToString() => GetFullName();
}

internal sealed class EnumerationAttributeTargetEqualityComparer : IEqualityComparer<EnumerationAttributeTarget>
{
	public static EnumerationAttributeTargetEqualityComparer FullName { get; } = new EnumerationAttributeTargetEqualityComparer();

	private EnumerationAttributeTargetEqualityComparer()
	{
	}

	public bool Equals(EnumerationAttributeTarget? x, EnumerationAttributeTarget? y)
	{
		if (ReferenceEquals(x, y))
		{
			return true;
		}

		if (x is not null)
		{
			if (y is not null)
			{
				return x.Namespace == y.Namespace &&
					x.Name == y.Name;
			}
			return false;
		}

		return y is null;
	}

	public int GetHashCode([DisallowNull] EnumerationAttributeTarget obj)
	{
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
		return HashCode.Combine(obj.Namespace, obj.Name);
#else
		return (obj.Namespace, obj.Name).GetHashCode();
#endif
	}
}
