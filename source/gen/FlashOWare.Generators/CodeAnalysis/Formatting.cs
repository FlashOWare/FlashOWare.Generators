namespace FlashOWare.CodeAnalysis;

internal static class Formatting
{
	internal static SymbolDisplayFormat Default { get; } = SymbolDisplayFormat.FullyQualifiedFormat
		.WithMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType);
}
