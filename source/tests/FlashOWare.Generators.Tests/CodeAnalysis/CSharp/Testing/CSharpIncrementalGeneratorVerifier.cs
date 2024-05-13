using System.Text;
using FlashOWare.Tests.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace FlashOWare.Tests.CodeAnalysis.CSharp.Testing;

internal static class CSharpIncrementalGeneratorVerifier<TIncrementalGenerator>
	where TIncrementalGenerator : IIncrementalGenerator, new()
{
	public static Task VerifyAsync(string source)
		=> VerifyAsync([source], default(ReadOnlySpan<DiagnosticResult>), default(ReadOnlySpan<(string filename, string content)>));

	public static Task VerifyAsync(ReadOnlySpan<string> sources)
		=> VerifyAsync(sources, default(ReadOnlySpan<DiagnosticResult>), default(ReadOnlySpan<(string filename, string content)>));

	public static Task VerifyAsync(string source, DiagnosticResult diagnostic)
		=> VerifyAsync([source], [diagnostic], default(ReadOnlySpan<(string filename, string content)>));

	public static Task VerifyAsync(ReadOnlySpan<string> sources, DiagnosticResult diagnostic)
		=> VerifyAsync(sources, [diagnostic], default(ReadOnlySpan<(string filename, string content)>));

	public static Task VerifyAsync(string source, ReadOnlySpan<DiagnosticResult> diagnostics)
		=> VerifyAsync([source], diagnostics, default(ReadOnlySpan<(string filename, string content)>));

	public static Task VerifyAsync(ReadOnlySpan<string> sources, ReadOnlySpan<DiagnosticResult> diagnostics)
		=> VerifyAsync(sources, diagnostics, default(ReadOnlySpan<(string filename, string content)>));

	public static Task VerifyAsync(string source, (string filename, string content) generated)
		=> VerifyAsync([source], default(ReadOnlySpan<DiagnosticResult>), [generated]);

	public static Task VerifyAsync(ReadOnlySpan<string> sources, (string filename, string content) generated)
		=> VerifyAsync(sources, default(ReadOnlySpan<DiagnosticResult>), [generated]);

	public static Task VerifyAsync(string source, ReadOnlySpan<(string filename, string content)> generated)
		=> VerifyAsync([source], default(ReadOnlySpan<DiagnosticResult>), generated);

	public static Task VerifyAsync(ReadOnlySpan<string> sources, ReadOnlySpan<(string filename, string content)> generated)
		=> VerifyAsync(sources, default(ReadOnlySpan<DiagnosticResult>), generated);

	public static Task VerifyAsync(string source, DiagnosticResult diagnostic, (string filename, string content) generated)
		=> VerifyAsync([source], [diagnostic], [generated]);

	public static Task VerifyAsync(ReadOnlySpan<string> sources, DiagnosticResult diagnostic, (string filename, string content) generated)
		=> VerifyAsync(sources, [diagnostic], [generated]);

	public static Task VerifyAsync(string source, ReadOnlySpan<DiagnosticResult> diagnostics, (string filename, string content) generated)
		=> VerifyAsync([source], diagnostics, [generated]);

	public static Task VerifyAsync(ReadOnlySpan<string> sources, ReadOnlySpan<DiagnosticResult> diagnostics, (string filename, string content) generated)
		=> VerifyAsync(sources, diagnostics, [generated]);

	public static Task VerifyAsync(string source, DiagnosticResult diagnostic, ReadOnlySpan<(string filename, string content)> generated)
		=> VerifyAsync([source], [diagnostic], generated);

	public static Task VerifyAsync(ReadOnlySpan<string> sources, DiagnosticResult diagnostic, ReadOnlySpan<(string filename, string content)> generated)
		=> VerifyAsync(sources, [diagnostic], generated);

	public static Task VerifyAsync(string source, ReadOnlySpan<DiagnosticResult> diagnostics, ReadOnlySpan<(string filename, string content)> generated)
		=> VerifyAsync([source], diagnostics, generated);

	public static Task VerifyAsync(ReadOnlySpan<string> sources, ReadOnlySpan<DiagnosticResult> diagnostics, ReadOnlySpan<(string filename, string content)> generated)
	{
		Span<(Type sourceGeneratorType, string filename, SourceText content)> documents = [];
		if (!generated.IsEmpty)
		{
			Type sourceGeneratorType = typeof(TIncrementalGenerator);

			documents = new (Type sourceGeneratorType, string filename, SourceText content)[generated.Length];
			for (int i = 0; i < generated.Length; i++)
			{
				(string filename, string content) = generated[i];
				documents[i] = (sourceGeneratorType, filename, SourceText.From(content, Encoding.UTF8, SourceHashAlgorithm.Sha1));
			}
		}

		return VerifyAsync(sources, diagnostics, documents);
	}

	private static Task VerifyAsync(ReadOnlySpan<string> sources, ReadOnlySpan<DiagnosticResult> diagnostics, ReadOnlySpan<(Type sourceGeneratorType, string filename, SourceText content)> generated)
	{
		CSharpIncrementalGeneratorTest<TIncrementalGenerator> test = new();

		test.TestState.Sources.AddRange(sources);
		test.TestState.GeneratedSources.AddRange(generated);
		test.TestState.ReferenceAssemblies = ReferenceAssemblies.Net.Net50;
		test.TestState.AdditionalReferences.Add(Assemblies.Attributes);

		test.ExpectedDiagnostics.AddRange(diagnostics);
		test.LanguageVersion = LanguageVersion.CSharp11;

		return test.RunAsync(CancellationToken.None);
	}
}
