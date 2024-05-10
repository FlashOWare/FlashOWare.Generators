using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace FlashOWare.Tests.CodeAnalysis.CSharp.Testing;

internal sealed class CSharpIncrementalGeneratorTest<TIncrementalGenerator> : CSharpSourceGeneratorTest<TIncrementalGenerator, XUnitVerifier>
	where TIncrementalGenerator : IIncrementalGenerator, new()
{
	public CSharpIncrementalGeneratorTest()
	{
		SolutionTransforms.Add(static (Solution solution, ProjectId projectId) =>
		{
			Project? project = solution.GetProject(projectId);
			Debug.Assert(project is not null, $"{nameof(ProjectId)} '{projectId}' is not an id of a project that is part of the {nameof(Solution)} {solution}.");

			CompilationOptions? compilationOptions = project.CompilationOptions;
			Debug.Assert(compilationOptions is not null, $"{nameof(CompilationOptions)} not found.");

			ImmutableDictionary<string, ReportDiagnostic> specificDiagnosticOptions = compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings);
			compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(specificDiagnosticOptions);
			solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

			return solution;
		});
	}

	public bool? CheckOverflow { get; init; }
	public LanguageVersion LanguageVersion { get; init; } = LanguageVersion.Default;

	protected override CompilationOptions CreateCompilationOptions()
	{
		var compilationOptions = (CSharpCompilationOptions)base.CreateCompilationOptions();

		if (CheckOverflow.HasValue)
		{
			compilationOptions = compilationOptions.WithOverflowChecks(CheckOverflow.Value);
		}

		return compilationOptions;
	}

	protected override ParseOptions CreateParseOptions()
	{
		var parseOptions = (CSharpParseOptions)base.CreateParseOptions();

		if (LanguageVersion is not LanguageVersion.Default)
		{
			parseOptions = parseOptions.WithLanguageVersion(LanguageVersion);
		}

		return parseOptions;
	}
}
