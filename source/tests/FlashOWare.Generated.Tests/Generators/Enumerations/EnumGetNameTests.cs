namespace FlashOWare.Tests.Generators.Enumerations;

public class EnumGetNameTests
{
	[Theory]
	[InlineData(-1, null)]
	[InlineData(0, nameof(MyEnum.Zero))]
	[InlineData(1, nameof(MyEnum.One))]
	[InlineData(2, nameof(MyEnum.Two))]
	[InlineData(3, nameof(MyEnum.Three))]
	[InlineData(4, nameof(MyEnum.Four))]
	[InlineData(5, null)]
	public void Enum_GetName(int value, string? expected)
	{
		var @enum = (MyEnum)value;

		string? system = SystemEnum.GetName(@enum);
		string? generated = GeneratedEnum.GetName(@enum);

		Assert.Multiple(
			() => Assert.Equal(expected, system),
			() => Assert.Equal(expected, generated)
		);

#if NET5_0_OR_GREATER
		string? intercepted = Enum.GetName(@enum);
		Assert.Equal(expected, intercepted);
#endif
	}

	[Theory]
	[InlineData(-1, null)]
	[InlineData(0, nameof(MyFlags.None))]
	[InlineData(1, nameof(MyFlags.First))]
	[InlineData(2, nameof(MyFlags.Second))]
	[InlineData(3, nameof(MyFlags.Third))]
	[InlineData(4, nameof(MyFlags.Fourth))]
	[InlineData(5, null)]
	[InlineData(6, null)]
	[InlineData(7, null)]
	[InlineData(8, null)]
	public void Enum_GetName_Flags(int value, string? expected)
	{
		var @enum = (MyFlags)value;

		string? system = SystemEnum.GetName(@enum);
		string? generated = GeneratedEnum.GetName(@enum);

		Assert.Multiple(
			() => Assert.Equal(expected, system),
			() => Assert.Equal(expected, generated)
		);

#if NET5_0_OR_GREATER
		string? intercepted = Enum.GetName(@enum);
		Assert.Equal(expected, intercepted);
#endif
	}

	[Fact]
	public void Enum_GetName_Empty()
	{
		MyEmpty @enum = default;

		string? system = SystemEnum.GetName(@enum);
		string? generated = GeneratedEnum.GetName(@enum);

		Assert.Multiple(
			() => Assert.Null(system),
			() => Assert.Null(generated)
		);

#if NET5_0_OR_GREATER
		string? intercepted = Enum.GetName(@enum);
		Assert.Null(intercepted);
#endif
	}
}
