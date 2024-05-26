namespace FlashOWare.Tests.Generators.Enumerations;

public partial class EnumIsDefinedTests
{
	[Theory]
	[InlineData(-1, false)]
	[InlineData(0, true)]
	[InlineData(1, true)]
	[InlineData(2, true)]
	[InlineData(3, true)]
	[InlineData(4, true)]
	[InlineData(5, false)]
	public void Enum_IsDefined(int value, bool expected)
	{
		var @enum = (MyEnum)value;

		bool system = SystemEnum.IsDefined(@enum);
		bool generated = GeneratedEnum.IsDefined(@enum);

		Assert.Multiple(
			() => Assert.Equal(expected, system),
			() => Assert.Equal(expected, generated)
		);

#if NET5_0_OR_GREATER
		bool intercepted = Enum.IsDefined(@enum);
		Assert.Equal(expected, intercepted);
#endif
	}

	[Theory]
	[InlineData(-1, false)]
	[InlineData(0, true)]
	[InlineData(1, true)]
	[InlineData(2, true)]
	[InlineData(3, true)]
	[InlineData(4, true)]
	[InlineData(5, false)]
	[InlineData(6, false)]
	[InlineData(7, false)]
	[InlineData(8, false)]
	public void Enum_IsDefined_Flags(int value, bool expected)
	{
		var @enum = (MyFlags)value;

		bool system = SystemEnum.IsDefined(@enum);
		bool generated = GeneratedEnum.IsDefined(@enum);

		Assert.Multiple(
			() => Assert.Equal(expected, system),
			() => Assert.Equal(expected, generated)
		);

#if NET5_0_OR_GREATER
		bool intercepted = Enum.IsDefined(@enum);
		Assert.Equal(expected, intercepted);
#endif
	}

	[Fact]
	public void Enum_IsDefined_Empty()
	{
		MyEmpty @enum = default;

		bool system = SystemEnum.IsDefined(@enum);
		bool generated = GeneratedEnum.IsDefined(@enum);

		Assert.Multiple(
			() => Assert.False(system),
			() => Assert.False(generated)
		);

#if NET5_0_OR_GREATER
		bool intercepted = Enum.IsDefined(@enum);
		Assert.False(intercepted);
#endif
	}
}
