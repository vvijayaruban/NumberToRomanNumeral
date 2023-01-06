using Shouldly;
using Xunit;

namespace NumberToRomanNumeral;

public class NumberToRomanNumeralSpec
{
    [Fact]
    public void _0_is_an_invalid_Roman_number()
    {
        Should.Throw<InvalidDataException>(() => Convert.ToRomanNumeral(0));
    }

    [Fact]
    public void Negatives_are_invalid_Roman_numbers()
    {
        Should.Throw<InvalidDataException>(() => Convert.ToRomanNumeral(-1));
    }

    [Theory]
    [InlineData(1000, "M")]
    [InlineData(500, "D")]
    [InlineData(100, "C")]
    [InlineData(50, "L")]
    [InlineData(10, "X")]
    [InlineData(5, "V")]
    [InlineData(1, "I")]
    public void Validate_Roman_Numerals_Denominations(int number, string result)
    {
        Convert.ToRomanNumeral(number).ShouldBe(result);
    }

    [Fact]
    public void Input_higher_than_the_supported_upper_limit_would_throw_ArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Convert.ToRomanNumeral(2001));
    }
}
