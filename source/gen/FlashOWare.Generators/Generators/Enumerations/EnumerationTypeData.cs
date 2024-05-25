using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace FlashOWare.Generators.Enumerations;

internal sealed class EnumerationTypeData(string name, ImmutableArray<string> constants) : IEquatable<EnumerationTypeData>
{
	internal string Name { get; } = name;
	internal ImmutableArray<string> Constants { get; } = constants;

	public bool Equals(EnumerationTypeData? other)
	{
		if (other is null)
		{
			return false;
		}
		else if (ReferenceEquals(this, other))
		{
			return true;
		}

		return Name == other.Name
			&& Constants.SequenceEqual(other.Constants);
	}

	public override bool Equals(object? obj) => Equals(obj as EnumerationTypeData);

	public override int GetHashCode() => throw new UnreachableException();
}

internal sealed class EnumerationTypeDataEqualityComparer : IEqualityComparer<EnumerationTypeData>
{
	public static EnumerationTypeDataEqualityComparer Name { get; } = new EnumerationTypeDataEqualityComparer();

	private EnumerationTypeDataEqualityComparer()
	{
	}

	public bool Equals(EnumerationTypeData? x, EnumerationTypeData? y)
	{
		if (ReferenceEquals(x, y))
		{
			return true;
		}

		if (x is null || y is null)
		{
			return false;
		}

		return x.Name == y.Name;
	}

	public int GetHashCode([DisallowNull] EnumerationTypeData obj)
	{
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER
		return obj.Name.GetHashCode(StringComparison.Ordinal);
#else
		return obj.Name.GetHashCode();
#endif
	}
}
