namespace FlashOWare.Generators.Enumerations;

internal static class EnumerationExamples
{
	internal static void Run()
	{
		Console.WriteLine($"{nameof(Enum.GetName)}: {Enum.GetName(StringComparison.OrdinalIgnoreCase)}");
		Console.WriteLine($"{nameof(Enum.IsDefined)}: {Enum.IsDefined(StringComparison.OrdinalIgnoreCase)}");
	}
}
