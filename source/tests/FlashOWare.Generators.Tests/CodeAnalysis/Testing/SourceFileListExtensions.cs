namespace FlashOWare.Tests.CodeAnalysis.Testing;

internal static class SourceFileListExtensions
{
	public static void AddRange(this SourceFileList list, ReadOnlySpan<string> source)
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

			foreach (string content in source)
			{
				list.Add(content);
			}
		}
	}
}
