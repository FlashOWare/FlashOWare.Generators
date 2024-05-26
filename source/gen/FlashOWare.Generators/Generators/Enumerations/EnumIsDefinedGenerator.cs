using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using FlashOWare.Attributes;
using FlashOWare.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FlashOWare.Generators.Enumerations;

[Generator(LanguageNames.CSharp)]
public sealed class EnumIsDefinedGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var source = context.SyntaxProvider.ForAttributeWithMetadataName(AttributeNames.EnumIsDefined,
			static bool (SyntaxNode node, CancellationToken cancellationToken) =>
			{
				return node is ClassDeclarationSyntax @class
					&& @class.Modifiers.Any(SyntaxKind.PartialKeyword);
			},
			static EnumerationAttributeTarget? (GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken) =>
			{
				Debug.Assert(context.TargetNode is ClassDeclarationSyntax);
				Debug.Assert(context.TargetSymbol is INamedTypeSymbol);
				Debug.Assert(!context.Attributes.IsEmpty);

				var node = Unsafe.As<ClassDeclarationSyntax>(context.TargetNode);
				var symbol = Unsafe.As<INamedTypeSymbol>(context.TargetSymbol);

				var attributes = symbol.GetAttributes().Intersect(context.Attributes, ReferenceEqualityComparer<AttributeData>.Instance);

				var methods = ImmutableArray.CreateBuilder<EnumerationTypeData>(context.Attributes.Length);
				foreach (AttributeData attribute in attributes)
				{
					Debug.Assert(attribute.AttributeClass is not null);
					Debug.Assert(attribute.AttributeClass.TypeArguments.Length == 1);

					ITypeSymbol enumeration = attribute.AttributeClass.TypeArguments[0];
					if (enumeration is IErrorTypeSymbol or not INamedTypeSymbol { EnumUnderlyingType: not null })
					{
						continue;
					}

					var constants = enumeration.GetMembers()
						.Where(static bool (ISymbol member) => member.Kind == SymbolKind.Field)
						.Select(static string (ISymbol field) => field.ToDisplayString(Formatting.Default))
						.ToImmutableArray();
					EnumerationTypeData method = new(enumeration.ToDisplayString(Formatting.Default), constants);
					methods.Add(method);
				}

				Debug.Assert(methods.Capacity <= context.Attributes.Length, $"Unexpected capacity growth from {context.Attributes.Length} to {methods.Capacity}.");
				if (methods.Count == 0)
				{
					return null;
				}

				string? @namespace = symbol.ContainingNamespace.IsGlobalNamespace ? null : symbol.ContainingNamespace.ToDisplayString();
				return new EnumerationAttributeTarget(@namespace, symbol.Name, methods.DrainToImmutable());
			})
			.WhereNotNull()
			.GroupBy(static (EnumerationAttributeTarget element) => (element.Namespace, element.Name),
				static (EnumerationAttributeTarget element) => element.Methods,
				static (key, elements) => new EnumerationAttributeTarget(key.Namespace, key.Name, elements.SelectMany(static elements => elements).Distinct(EnumerationTypeDataEqualityComparer.Name).ToImmutableArray()))
			.Distinct(EnumerationAttributeTargetEqualityComparer.FullName);

		context.RegisterSourceOutput(source, static void (SourceProductionContext context, EnumerationAttributeTarget source) =>
		{
			StringBuilder builder = new();
			using StringWriter writer = new(builder, CultureInfo.InvariantCulture);
			using IndentedTextWriter document = new(writer, "\t");

			document.WriteLine("// <auto-generated/>");
			document.WriteLine("#nullable enable");
			document.WriteLineNoTabs(null);

			if (!source.IsGlobalNamespace)
			{
				document.WriteLine($"namespace {source.Namespace};");
				document.WriteLineNoTabs(null);
			}

			document.WriteLine($"partial class {source.Name}");
			document.WriteLine('{');
			document.Indent++;

			for (int i = 0; i < source.Methods.Length; i++)
			{
				EnumerationTypeData method = source.Methods[i];

				document.WriteLine(Sources.GeneratedCodeAttribute);
				document.WriteLine($"public static bool {Methods.IsDefined}({method.Name} value)");
				EnumerationWriter.WriteIsDefinedMethodBody(document, method);

				if (i + 1 < source.Methods.Length)
				{
					document.WriteLineNoTabs(null);
				}
			}

			document.Indent--;
			document.WriteLine('}');

			Debug.Assert(document.Indent == 0);

			context.AddSource($"{source.GetFullName()}.{Methods.IsDefined}.g.cs", writer.ToString());
		});
	}
}
