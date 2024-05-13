using Microsoft.CodeAnalysis.Text;

namespace FlashOWare.Tests.CodeAnalysis.Testing;

internal static class SourceFileCollectionExtensions
{
	public static void AddRange(this SourceFileCollection list, ReadOnlySpan<(Type sourceGeneratorType, string filename, SourceText content)> source)
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

			foreach ((Type sourceGeneratorType, string filename, SourceText content) file in source)
			{
				list.Add(file);
			}
		}
	}
}
