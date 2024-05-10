using Verifier = FlashOWare.Tests.CodeAnalysis.CSharp.Testing.CSharpIncrementalGeneratorVerifier<FlashOWare.Generators.Enumerations.EnumInterceptorGenerator>;

namespace FlashOWare.Tests.Generators.Enumerations;

public class EnumInterceptorGeneratorTests
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
