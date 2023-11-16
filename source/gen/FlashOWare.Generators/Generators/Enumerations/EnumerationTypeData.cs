using System.Collections.Immutable;
using System.Diagnostics;

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
