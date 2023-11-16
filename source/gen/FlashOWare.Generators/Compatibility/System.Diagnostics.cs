namespace System.Diagnostics;

#if !NET7_0_OR_GREATER
internal sealed class UnreachableException : Exception
{
	public UnreachableException()
		: base("The program executed an instruction that was thought to be unreachable.")
	{
	}

	public UnreachableException(string? message)
		: base(message)
	{
	}

	public UnreachableException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
#endif
