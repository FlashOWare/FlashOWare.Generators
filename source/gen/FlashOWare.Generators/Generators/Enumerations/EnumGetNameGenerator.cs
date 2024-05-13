using System.CodeDom.Compiler;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using FlashOWare.Attributes;
using FlashOWare.CodeAnalysis;
using FlashOWare.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FlashOWare.Generators.Enumerations;

[Generator(LanguageNames.CSharp)]
public sealed class EnumGetNameGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var source = context.SyntaxProvider.ForAttributeWithMetadataName(AttributeNames.EnumGetName,
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

				var methods = ImmutableArray.CreateBuilder<EnumerationTypeData>(context.Attributes.Length);
				foreach (AttributeData attribute in context.Attributes)
				{
					Debug.Assert(attribute.AttributeClass is not null);
					Debug.Assert(attribute.AttributeClass.TypeArguments.Length == 1);

					ITypeSymbol enumeration = attribute.AttributeClass.TypeArguments[0];
					if (enumeration is IErrorTypeSymbol and not INamedTypeSymbol { EnumUnderlyingType: not null })
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

				if (methods.Count == 0)
				{
					return null;
				}

				string? @namespace = symbol.ContainingNamespace.IsGlobalNamespace ? null : symbol.ContainingNamespace.ToDisplayString();
				return new EnumerationAttributeTarget(@namespace, symbol.Name, methods.DrainToImmutable());
			})
			.Where(static bool (EnumerationAttributeTarget? value) => value is not null);

		context.RegisterSourceOutput(source!, static void (SourceProductionContext context, EnumerationAttributeTarget source) =>
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
				document.WriteLine($"public static string? {Methods.GetName}({method.Name} value)");
				EnumerationWriter.WriteGetNameMethodBody(document, method);

				if (i + 1 < source.Methods.Length)
				{
					document.WriteLineNoTabs(null);
				}
			}

			document.Indent--;
			document.WriteLine('}');

			Debug.Assert(document.Indent == 0);

			context.AddSource($"{source.GetFullName()}.{Methods.GetName}.g.cs", writer.ToString());
		});
	}
}
