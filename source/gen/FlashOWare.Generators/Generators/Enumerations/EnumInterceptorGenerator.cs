using System.CodeDom.Compiler;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using FlashOWare.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FlashOWare.Generators.Enumerations;

[Generator(LanguageNames.CSharp)]
public sealed class EnumInterceptorGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var source = context.SyntaxProvider.CreateSyntaxProvider(
			static bool (SyntaxNode node, CancellationToken CancellationToken) =>
			{
				return node is InvocationExpressionSyntax invocation
					&& invocation.Expression is MemberAccessExpressionSyntax
					&& invocation is { ArgumentList.Arguments.Count: 1 };
			},
			static EnumMethodInvocation? (GeneratorSyntaxContext context, CancellationToken cancellationToken) =>
			{
				Debug.Assert(context.Node is InvocationExpressionSyntax);
				var invocation = Unsafe.As<InvocationExpressionSyntax>(context.Node);

				Debug.Assert(invocation.Expression is MemberAccessExpressionSyntax);
				var access = Unsafe.As<MemberAccessExpressionSyntax>(invocation.Expression);

				SymbolInfo info = context.SemanticModel.GetSymbolInfo(invocation, cancellationToken);
				if (info.Symbol is IMethodSymbol method)
				{
					INamedTypeSymbol? targetType = context.SemanticModel.Compilation.GetTypeByMetadataName("System.Enum");

					if (method.MethodKind == MethodKind.Ordinary &&
						method.ContainingType.Equals(targetType, SymbolEqualityComparer.Default))
					{
						if (method.DeclaredAccessibility == Accessibility.Public &&
							method.IsStatic &&
							method.IsGenericMethod &&
							method.TypeArguments is [{ TypeKind: TypeKind.Enum }] &&
							method.TypeParameters is [_] &&
							method.Parameters is [{ Type: INamedTypeSymbol type }])
						{
							if (method.ReturnType.SpecialType != SpecialType.System_String &&
								method.ReturnType.SpecialType != SpecialType.System_Boolean)
							{
								return null;
							}

							if (type is IErrorTypeSymbol and not INamedTypeSymbol { EnumUnderlyingType: not null })
							{
								return null;
							}

							FileLinePositionSpan lineSpan = access.SyntaxTree.GetLineSpan(access.Name.Span, cancellationToken);
							string filePath = invocation.SyntaxTree.GetInterceptorFilePath(context.SemanticModel.Compilation);
							int line = lineSpan.StartLinePosition.Line + 1;
							int character = lineSpan.StartLinePosition.Character + 1;

							var constants = type.GetMembers()
								.Where(static bool (ISymbol member) => member.Kind == SymbolKind.Field)
								.Select(static string (ISymbol field) => field.ToDisplayString(Formatting.Default))
								.ToImmutableArray();
							EnumerationTypeData @enum = new(type.ToDisplayString(Formatting.Default), constants);
							InterceptsLocationInfo location = new(filePath, line, character);

							return new EnumMethodInvocation(method.Name, @enum, location);
						}
					}
				}

				return null;
			})
			.WhereNotNull()
			.Collect();

		context.RegisterImplementationSourceOutput(source, static void (SourceProductionContext context, ImmutableArray<EnumMethodInvocation> source) =>
		{
			if (source.IsEmpty)
			{
				return;
			}

			StringBuilder builder = new();
			using StringWriter writer = new(builder, CultureInfo.InvariantCulture);
			using IndentedTextWriter document = new(writer, "\t");

			document.WriteLine("// <auto-generated/>");
			document.WriteLine("#nullable enable");
			document.WriteLineNoTabs(null);
			document.WriteLine(Sources.InterceptsLocationAttribute);
			document.WriteLineNoTabs(null);

			document.WriteLine($"namespace {Namespaces.Generated}");
			document.WriteLine('{');
			document.Indent++;
			document.WriteLine("using System.Runtime.CompilerServices;");
			document.WriteLineNoTabs(null);
			document.WriteLine(Sources.GeneratedCodeAttribute);
			document.WriteLine("file static class EnumInterceptors");
			document.WriteLine('{');
			document.Indent++;

			IEnumerable<IGrouping<string, EnumMethodInvocation>> group = source.GroupBy(static value => value.Name);

			foreach (var invocations in group)
			{
				int index = 0;

				foreach (var invocation in invocations)
				{
					document.WriteLine($"""[InterceptsLocation(@"{invocation.Location.FilePath}", {invocation.Location.Line}, {invocation.Location.Character})]""");

					if (invocation.Name == Methods.GetName)
					{
						document.WriteLine($"internal static string? {Methods.GetName}{index++}({invocation.Enum.Name} value)");
						EnumerationWriter.WriteGetNameMethodBody(document, invocation.Enum);
					}
					else if (invocation.Name == Methods.IsDefined)
					{
						document.WriteLine($"internal static bool {Methods.IsDefined}{index++}({invocation.Enum.Name} value)");
						EnumerationWriter.WriteIsDefinedMethodBody(document, invocation.Enum);
					}
				}
			}

			document.Indent--;
			document.WriteLine('}');
			document.Indent--;
			document.WriteLine('}');

			Debug.Assert(document.Indent == 0);
			context.AddSource($"{Namespaces.Generated}.EnumInterceptors.g.cs", writer.ToString());
		});
	}
}

file sealed record class EnumMethodInvocation(string Name, EnumerationTypeData Enum, InterceptsLocationInfo Location);

file sealed record class InterceptsLocationInfo(string FilePath, int Line, int Character);
