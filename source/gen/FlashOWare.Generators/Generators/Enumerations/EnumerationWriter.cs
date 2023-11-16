using System.CodeDom.Compiler;

namespace FlashOWare.Generators.Enumerations;

internal static class EnumerationWriter
{
	internal static void WriteGetNameMethodBody(IndentedTextWriter document, EnumerationTypeData value)
	{
		document.WriteLine('{');
		document.Indent++;

		document.WriteLine("return value switch");
		document.WriteLine('{');
		document.Indent++;

		foreach (string constant in value.Constants)
		{
			document.WriteLine($"{constant} => nameof({constant}),");
		}

		document.WriteLine("_ => null,");
		document.Indent--;
		document.WriteLine("};");

		document.Indent--;
		document.WriteLine('}');
	}

	internal static void WriteIsDefinedMethodBody(IndentedTextWriter document, EnumerationTypeData value)
	{
		document.WriteLine('{');
		document.Indent++;

		if (value.Constants.IsEmpty)
		{
			document.WriteLine("return false;");
		}
		else
		{
			document.WriteLine("return value is");
			document.Indent++;
			for (int i = 0; i < value.Constants.Length; i++)
			{
				string constant = value.Constants[i];

				document.Write(constant);
				if (i + 1 < value.Constants.Length)
				{
					document.WriteLine(" or");
				}
				else
				{
					document.WriteLine(';');
				}
			}
			document.Indent--;
		}

		document.Indent--;
		document.WriteLine('}');
	}
}
