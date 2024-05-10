using System.Reflection;

namespace FlashOWare.Text;

internal static class Sources
{
	private static readonly AssemblyName assemblyName = typeof(Sources).Assembly.GetName();

	internal static string GeneratedCodeAttribute { get; } = $"""
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{assemblyName.Name}", "{assemblyName.Version}")]
		""";

	internal static string InterceptsLocationAttribute { get; } = $$"""
		namespace System.Runtime.CompilerServices
		{
			{{GeneratedCodeAttribute}}
			[global::System.AttributeUsage(global::System.AttributeTargets.Method, AllowMultiple = true)]
			file sealed class InterceptsLocationAttribute : global::System.Attribute
			{
				public InterceptsLocationAttribute(string filePath, int line, int character)
				{
				}
			}
		}
		""";
}
