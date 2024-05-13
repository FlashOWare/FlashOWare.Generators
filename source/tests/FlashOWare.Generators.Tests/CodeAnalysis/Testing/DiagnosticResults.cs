using Microsoft.CodeAnalysis;

namespace FlashOWare.Tests.CodeAnalysis.Testing;

internal static class DiagnosticResults
{
	public static DiagnosticResult Create()
		=> new();

	public static DiagnosticResult Create(string id, DiagnosticSeverity severity)
		=> new(id, severity);

	public static DiagnosticResult Create(DiagnosticDescriptor descriptor)
		=> new(descriptor);

	public static DiagnosticResult CS0019(int markupKey, char @operator, string operand, string type)
	{
		return new DiagnosticResult("CS0019", DiagnosticSeverity.Error)
			.WithMessageFormat("Operator '{0}' cannot be applied to operands of type '{1}' and '{2}'")
			.WithArguments(@operator, operand, type)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0103(int markupKey, string name)
	{
		return new DiagnosticResult("CS0103", DiagnosticSeverity.Error)
			.WithMessageFormat("The name '{0}' does not exist in the current context")
			.WithArguments(name)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0246(int markupKey, string identifier)
	{
		return new DiagnosticResult("CS0246", DiagnosticSeverity.Error)
			.WithMessageFormat("The type or namespace name '{0}' could not be found (are you missing a using directive or an assembly reference?)")
			.WithArguments(identifier)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0305(int markupKey, string group, int arguments)
	{
		return new DiagnosticResult("CS0305", DiagnosticSeverity.Error)
			.WithMessageFormat("Using the generic method group '{0}' requires {1} type arguments")
			.WithArguments(group, arguments)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0315(int markupKey, string type, string parameter, string generic, string from, string to)
	{
		return new DiagnosticResult("CS0315", DiagnosticSeverity.Error)
			.WithMessageFormat("The type '{0}' cannot be used as type parameter '{1}' in the generic type or method '{2}'. There is no boxing conversion from '{3}' to '{4}'.")
			.WithArguments(type, parameter, generic, from, to)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS1003(int markupKey, char token)
	{
		return new DiagnosticResult("CS1003", DiagnosticSeverity.Error)
			.WithMessageFormat("Syntax error, '{0}' expected")
			.WithArguments(token)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS1031(int markupKey)
	{
		return new DiagnosticResult("CS1031", DiagnosticSeverity.Error)
			.WithMessage("Type expected")
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS1501(int markupKey, string method, int arguments)
	{
		return new DiagnosticResult("CS1501", DiagnosticSeverity.Error)
			.WithMessageFormat("No overload for method '{0}' takes {1} arguments")
			.WithArguments(method, arguments)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS1525(int markupKey, char term)
	{
		return new DiagnosticResult("CS1525", DiagnosticSeverity.Error)
			.WithMessageFormat("Invalid expression term '{0}'")
			.WithArguments(term)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS7003(int markupKey)
	{
		return new DiagnosticResult("CS7003", DiagnosticSeverity.Error)
			.WithMessage("Unexpected use of an unbound generic name")
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS7036(int markupKey, string parameter, string method)
	{
		return new DiagnosticResult("CS7036", DiagnosticSeverity.Error)
			.WithMessageFormat("There is no argument given that corresponds to the required parameter '{0}' of '{1}'")
			.WithArguments(parameter, method)
			.WithLocation(markupKey);
	}
}
