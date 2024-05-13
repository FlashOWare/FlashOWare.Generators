namespace FlashOWare.CodeAnalysis;

internal static class IncrementalValuesProviderExtensions
{
	public static IncrementalValuesProvider<TSource> WhereNotNull<TSource>(this IncrementalValuesProvider<TSource?> source)
		where TSource : class
		=> source.Where(static bool (TSource? value) => value is not null)!;
}
