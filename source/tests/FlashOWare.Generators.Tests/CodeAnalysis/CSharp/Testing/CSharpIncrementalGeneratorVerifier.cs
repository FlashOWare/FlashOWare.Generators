using System.Text;
using FlashOWare.Tests.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

namespace FlashOWare.Tests.CodeAnalysis.CSharp.Testing;

internal static class CSharpIncrementalGeneratorVerifier<TIncrementalGenerator>
	where TIncrementalGenerator : IIncrementalGenerator, new()
{
	public static async Task VerifyAsync(string source)
	{
		CSharpIncrementalGeneratorTest<TIncrementalGenerator> test = new()
		{
			TestCode = source,
			ReferenceAssemblies = ReferenceAssemblies.Net.Net50,
		};

		test.TestState.AdditionalReferences.Add(Assemblies.Attributes);

		test.LanguageVersion = LanguageVersion.CSharp11;

		await test.RunAsync(CancellationToken.None);
	}

	public static async Task VerifyAsync(string source, (string filename, string content) generatedSource)
	{
		CSharpIncrementalGeneratorTest<TIncrementalGenerator> test = new()
		{
			TestState =
			{
				Sources = { source },
				GeneratedSources =
				{
					(typeof(TIncrementalGenerator), generatedSource.filename, SourceText.From(generatedSource.content, Encoding.UTF8, SourceHashAlgorithm.Sha1)),
				},
				ReferenceAssemblies = ReferenceAssemblies.Net.Net50,
			}
		};

		test.TestState.AdditionalReferences.Add(Assemblies.Attributes);

		test.LanguageVersion = LanguageVersion.CSharp11;

		await test.RunAsync(CancellationToken.None);
	}
}
