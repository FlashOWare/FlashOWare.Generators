namespace FlashOWare.CodeAnalysis;

internal static class SyntaxTreeExtensions
{
	internal static string GetInterceptorFilePath(this SyntaxTree tree, Compilation compilation)
	{
		return compilation.Options.SourceReferenceResolver?.NormalizePath(tree.FilePath, baseFilePath: null) ?? tree.FilePath;
	}
}
