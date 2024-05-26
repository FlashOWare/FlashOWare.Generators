namespace FlashOWare.Collections.Generic;

internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T>
	where T : class?
{
	public static ReferenceEqualityComparer<T> Instance { get; } = new ReferenceEqualityComparer<T>();

	private ReferenceEqualityComparer()
	{
	}

	public bool Equals(T? x, T? y) => ReferenceEquals(x, y);
	public int GetHashCode([DisallowNull] T obj) => obj.GetHashCode();
}
