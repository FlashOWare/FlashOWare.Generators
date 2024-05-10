using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FlashOWare.Tests.CodeAnalysis.CSharp;

internal static class CSharpVerifierHelper
{
	internal static ImmutableDictionary<string, ReportDiagnostic> NullableWarnings { get; } = GetNullableWarningsFromCompiler();

	private static ImmutableDictionary<string, ReportDiagnostic> GetNullableWarningsFromCompiler()
	{
		string[] args = ["/warnaserror:nullable"];
		CSharpCommandLineArguments commandLineArguments = CSharpCommandLineParser.Default.Parse(args, Environment.CurrentDirectory, Environment.CurrentDirectory, null);
		ImmutableDictionary<string, ReportDiagnostic> nullableWarnings = commandLineArguments.CompilationOptions.SpecificDiagnosticOptions;

		return nullableWarnings;
	}
}
