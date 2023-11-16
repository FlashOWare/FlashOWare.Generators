namespace FlashOWare.CodeAnalysis;

internal static class CompilationExtensions
{
	internal static string GetInterceptorFilePath(Compilation compilation, SyntaxTree tree)
	{
		return compilation.Options.SourceReferenceResolver?.NormalizePath(tree.FilePath, baseFilePath: null) ?? tree.FilePath;
	}
}
