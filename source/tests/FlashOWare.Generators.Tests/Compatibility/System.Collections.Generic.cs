namespace System.Collections.Generic;

#if !NET8_0_OR_GREATER
internal static class CollectionExtensions
{
	public static void AddRange<T>(this List<T> list, ReadOnlySpan<T> source)
	{
		if (!source.IsEmpty)
		{
			if (list.Capacity - list.Count < source.Length)
			{
#if NET6_0_OR_GREATER
				_ = list.EnsureCapacity(checked(list.Count + source.Length));
#else
				list.Capacity = checked(list.Count + source.Length);
#endif
			}

			foreach (T item in source)
			{
				list.Add(item);
			}
		}
	}
}
#endif
