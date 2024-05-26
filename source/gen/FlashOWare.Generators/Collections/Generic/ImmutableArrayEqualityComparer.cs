namespace FlashOWare.Collections.Generic;

internal sealed class ImmutableArrayEqualityComparer<T> : IEqualityComparer<ImmutableArray<T>>
{
	public static ImmutableArrayEqualityComparer<T> Null { get; } = new ImmutableArrayEqualityComparer<T>();

	private ImmutableArrayEqualityComparer()
	{
	}

	public bool Equals(ImmutableArray<T> x, ImmutableArray<T> y) => false;
	public int GetHashCode(ImmutableArray<T> obj) => throw new UnreachableException();
}
