using System.Text;
using Microsoft.CodeAnalysis;
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
		};

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
					(typeof(TIncrementalGenerator), generatedSource.filename, SourceText.From(generatedSource.content, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
				},
			}
		};

		await test.RunAsync(CancellationToken.None);
	}
}
