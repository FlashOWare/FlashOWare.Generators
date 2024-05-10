using Verifier = FlashOWare.Tests.CodeAnalysis.CSharp.Testing.CSharpIncrementalGeneratorVerifier<FlashOWare.Generators.Enumerations.EnumIsDefinedGenerator>;

namespace FlashOWare.Tests.Generators.Enumerations;

public class EnumIsDefinedGeneratorTests
{
	[Fact]
	public async Task Execute_NoAttributes_NoEmit()
	{
		string code = """
			using System;
			using FlashOWare.Generators;

			namespace Namespace;

			public class Class
			{
				public void Method()
				{
					_ = Enum.IsDefined(DateTimeKind.Utc);
				}
			}
			""";

		await Verifier.VerifyAsync(code);
	}

	[Fact]
	public async Task Execute_WithAttributes_AddSource()
	{
		string code = """
			using System;
			using FlashOWare.Generators;

			namespace Namespace;

			[GeneratedEnumIsDefinedAttribute<DateTimeKind>]
			public static partial class Class
			{
			}
			""";

		string generated = $$"""
			{{AutoGenerated.Header}}
			namespace Namespace;

			partial class Class
			{
				{{AutoGenerated.GeneratedCodeAttribute}}
				public static bool IsDefined(global::System.DateTimeKind value)
				{
					return value is
						global::System.DateTimeKind.Unspecified or
						global::System.DateTimeKind.Utc or
						global::System.DateTimeKind.Local;
				}
			}

			""";

		await Verifier.VerifyAsync(code, ("Namespace.Class.IsDefined.g.cs", generated));
	}

	[Fact]
	public async Task Execute_GlobalNamespace_AddSource()
	{
		string code = """
			using System;
			using FlashOWare.Generators;

			[GeneratedEnumIsDefinedAttribute<DateTimeKind>]
			public static partial class Class
			{
			}
			""";

		string generated = $$"""
			{{AutoGenerated.Header}}
			partial class Class
			{
				{{AutoGenerated.GeneratedCodeAttribute}}
				public static bool IsDefined(global::System.DateTimeKind value)
				{
					return value is
						global::System.DateTimeKind.Unspecified or
						global::System.DateTimeKind.Utc or
						global::System.DateTimeKind.Local;
				}
			}

			""";

		await Verifier.VerifyAsync(code, ("Class.IsDefined.g.cs", generated));
	}
}
