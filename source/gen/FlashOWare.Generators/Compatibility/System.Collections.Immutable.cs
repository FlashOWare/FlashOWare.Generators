namespace System.Collections.Immutable;

#if !NET8_0_OR_GREATER
internal static class ImmutableArrayBuilderExtensions
{
	// >= Microsoft.CodeAnalysis.CSharp v4.9.2
	// >= System.Collections.Immutable v8.0.0
	public static ImmutableArray<T> DrainToImmutable<T>(this ImmutableArray<T>.Builder builder)
	{
		if (builder.Capacity == builder.Count)
		{
			return builder.MoveToImmutable();
		}

		ImmutableArray<T> result = builder.ToImmutable();
		builder.Count = 0;
		builder.Capacity = 0;
		return result;
	}
}
#endif
