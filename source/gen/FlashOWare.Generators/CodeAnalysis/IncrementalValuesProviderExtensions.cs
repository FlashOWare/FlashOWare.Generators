using System.Collections.Immutable;
using FlashOWare.Collections.Generic;

namespace FlashOWare.CodeAnalysis;

internal static class IncrementalValuesProviderExtensions
{
	public static IncrementalValuesProvider<TSource> WhereNotNull<TSource>(this IncrementalValuesProvider<TSource?> source)
		where TSource : class
		=> source.Where(static bool (TSource? value) => value is not null)!;

	public static IncrementalValuesProvider<TSource> Distinct<TSource>(this IncrementalValuesProvider<TSource> source, IEqualityComparer<TSource> comparer)
	{
		return source
			.Collect().WithComparer(ImmutableArrayEqualityComparer<TSource>.Null)
			.SelectMany(ImmutableArray<TSource> (ImmutableArray<TSource> values, CancellationToken _) =>
			{
				if (values.IsEmpty)
				{
					return values;
				}

				ImmutableArray<TSource>.Builder results = ImmutableArray.CreateBuilder<TSource>(values.Length);

#if NETSTANDARD2_1_OR_GREATER || NET472_OR_GREATER || NETCOREAPP2_0_OR_GREATER
				HashSet<TSource> set = new(values.Length, comparer);
#else
				HashSet<TSource> set = new(comparer);
#endif
				foreach (TSource value in values)
				{
					if (set.Add(value))
					{
						results.Add(value);
					}
				}

				return results.DrainToImmutable();
			});
	}
}
