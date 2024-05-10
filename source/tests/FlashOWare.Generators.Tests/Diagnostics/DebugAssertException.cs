#if DEBUG && NETFRAMEWORK
namespace FlashOWare.Tests.Diagnostics;

[SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "DEBUG && NETFRAMEWORK")]
[SuppressMessage("Design", "CA1064:Exceptions should be public", Justification = "DEBUG && NETFRAMEWORK")]
internal sealed class DebugAssertException : Exception
{
	public DebugAssertException(string? message)
		: base(message)
	{
	}

	public DebugAssertException(string? message, string? detailMessage)
		: base($"{message}{Environment.NewLine}{detailMessage}")
	{
	}
}
#endif
