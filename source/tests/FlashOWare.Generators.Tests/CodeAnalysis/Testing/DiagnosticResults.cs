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

	public static DiagnosticResult CS0305(int markupKey, string generic, string group, int arguments)
	{
		return new DiagnosticResult("CS0305", DiagnosticSeverity.Error)
			.WithMessageFormat("Using the generic {0} '{1}' requires {2} type arguments")
			.WithArguments(generic, group, arguments)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0315(int markupKey, string type, string parameter, string generic, string from, string to)
	{
		return new DiagnosticResult("CS0315", DiagnosticSeverity.Error)
			.WithMessageFormat("The type '{0}' cannot be used as type parameter '{1}' in the generic type or method '{2}'. There is no boxing conversion from '{3}' to '{4}'.")
			.WithArguments(type, parameter, generic, from, to)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0411(int markupKey, string method)
	{
		return new DiagnosticResult("CS0411", DiagnosticSeverity.Error)
			.WithMessageFormat("The type arguments for method '{0}' cannot be inferred from the usage. Try specifying the type arguments explicitly.")
			.WithArguments(method)
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS0592(int markupKey, string attribute, params string[] declarations)
	{
		return new DiagnosticResult("CS0592", DiagnosticSeverity.Error)
			.WithMessageFormat("Attribute '{0}' is not valid on this declaration type. It is only valid on '{1}' declarations.")
			.WithArguments(attribute, String.Join(", ", declarations))
			.WithLocation(markupKey);
	}

	public static DiagnosticResult CS1001(int markupKey)
	{
		return new DiagnosticResult("CS1001", DiagnosticSeverity.Error)
			.WithMessage("Identifier expected")
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

	public static DiagnosticResult CS1503(int markupKey, string from, string to)
	{
		return new DiagnosticResult("CS1503", DiagnosticSeverity.Error)
			.WithMessageFormat("Argument 1: cannot convert from '{0}' to '{1}'")
			.WithArguments(from, to)
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
