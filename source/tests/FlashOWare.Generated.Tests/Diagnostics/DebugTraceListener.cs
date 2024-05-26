#if DEBUG && NETFRAMEWORK
using System.Diagnostics;

namespace FlashOWare.Tests.Diagnostics;

internal sealed class DebugTraceListener : TraceListener
{
	public DebugTraceListener()
		: base(nameof(Debug))
	{
	}

	public DebugTraceListener(string? name)
		: base(name)
	{
	}

	public override void Write(string? message)
		=> throw new NotImplementedException($"{nameof(DebugTraceListener)}.{nameof(Write)}({nameof(String)})");

	public override void WriteLine(string? message)
		=> throw new NotImplementedException($"{nameof(DebugTraceListener)}.{nameof(WriteLine)}({nameof(String)})");

	public override void Fail(string? message)
		=> throw new DebugAssertException(message);

	public override void Fail(string? message, string? detailMessage)
		=> throw new DebugAssertException(message, detailMessage);
}
#endif
