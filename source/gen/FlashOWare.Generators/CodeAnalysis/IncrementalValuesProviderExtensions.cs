namespace FlashOWare.CodeAnalysis;

internal static class IncrementalValuesProviderExtensions
{
	public static IncrementalValuesProvider<TSource> Distinct<TSource>(this IncrementalValuesProvider<TSource> source, IEqualityComparer<TSource> comparer)
	{
		return source
			.Collect().WithComparer(ImmutableArrayEqualityComparer<TSource>.Null)
			.SelectMany(ImmutableArray<TSource> (ImmutableArray<TSource> values, CancellationToken cancellationToken) =>
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

	public static IncrementalValuesProvider<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IncrementalValuesProvider<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		where TKey : notnull, IEquatable<TKey>
	{
		return source
			.Collect().WithComparer(ImmutableArrayEqualityComparer<TSource>.Null)
			.SelectMany(ImmutableArray<TResult> (ImmutableArray<TSource> values, CancellationToken cancellationToken) =>
			{
				if (values.IsEmpty)
				{
					return ImmutableArray<TResult>.Empty;
				}

				Dictionary<TKey, List<TElement>> lookup = new(values.Length);
				foreach (TSource value in values)
				{
					TKey key = keySelector(value);
					TElement element = elementSelector(value);
					if (lookup.TryGetValue(key, out List<TElement>? elements))
					{
						elements.Add(element);
					}
					else
					{
						elements = new(0) { element };
						lookup.Add(key, elements);
					}
				}

				ImmutableArray<TResult>.Builder results = ImmutableArray.CreateBuilder<TResult>(lookup.Count);
				foreach (KeyValuePair<TKey, List<TElement>> item in lookup)
				{
					TResult result = resultSelector(item.Key, item.Value);
					results.Add(result);
				}

				Debug.Assert(results.Capacity == results.Count, $"Count ({results.Count}) does not equal Capacity ({results.Capacity}).");
				return results.DrainToImmutable();
			});
	}

	public static IncrementalValuesProvider<TSource> WhereNotNull<TSource>(this IncrementalValuesProvider<TSource?> source)
		where TSource : class
		=> source.Where(static bool (TSource? value) => value is not null)!;
}
