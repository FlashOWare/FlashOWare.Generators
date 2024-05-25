namespace FlashOWare.Tests.Generators.Enumerations;

internal enum MyEnum
{
	Zero = 0,
	One = 1,
	Two = 2,
	Three = 3,
	Four = 4,
}

[Flags]
internal enum MyFlags
{
	None = 0,
	First = 1,
	Second = 2,
	Third = First | Second,
	Fourth = 4,
}

internal enum MyEmpty;
