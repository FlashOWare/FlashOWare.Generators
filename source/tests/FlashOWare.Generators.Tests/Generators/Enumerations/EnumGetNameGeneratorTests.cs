using Verifier = FlashOWare.Tests.CodeAnalysis.CSharp.Testing.CSharpIncrementalGeneratorVerifier<FlashOWare.Generators.Enumerations.EnumGetNameGenerator>;

namespace FlashOWare.Tests.Generators.Enumerations;

public class EnumGetNameGeneratorTests
{
	[Fact]
	public async Task First_TODO()
	{
		string code = """

			""";

		await Verifier.VerifyAsync(code);
	}

	[Fact]
	public async Task Second_TODO()
	{
		string code = """

			""";

		string generated = """

			""";

		await Verifier.VerifyAsync(code, ("TODO", generated));
	}
}
