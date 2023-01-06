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
    public void Standard_Roman_Numerals_Denominations(int number, string result)
    {
        Convert.ToRomanNumeral(number).ShouldBe(result);
    }

    [Fact]
    public void Input_higher_than_supported_upper_limit_would_throw_ArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Convert.ToRomanNumeral(2001));
    }

    [Theory]
    [InlineData(1000, "M")]
    [InlineData(1500, "MD")]
    public void Two_or_more_decimal_digits_would_give_Roman_numeral_by_appending_each_from_highest_to_lowest(int number, string result)
    {
        Convert.ToRomanNumeral(number).ShouldBe(result);
    }

    public static TheoryData<string, int, string> ReadIndividualDecimalPlacesTable()
    {
        var data = new TheoryData<string, int, string>();
        var fileContent = TestDataProvider
            .DataForIndividualDecimalPlaces;
        var lines = fileContent.Split(Environment.NewLine);
        var headings = lines.First().Split('\t').ToList();

        foreach (var (line, index) in lines.Skip(1).Select((x, i) => (x, i)))
        {
            var lingSplits = line.Split('\t');
            var digit = int.Parse(lingSplits.First().Trim());

            if (index < 2)
            {
                data.Add(headings[1], digit, lingSplits[1]);
            }

            data.Add(headings[2], digit, lingSplits[2]);
            data.Add(headings[3], digit, lingSplits[3]);
            data.Add(headings[4], digit, lingSplits[4]);
        }

        return data;
    }

    [Theory, MemberData(nameof(ReadIndividualDecimalPlacesTable))]
    public void
        Testing_numbers_containing_two_or_more_decimal_digits_is_built_by_appending_the_Roman_numeral_equivalent_for_each_from_highest_to_lowest2(
            string decimalPlace, int digit, string roman)
    {
        Convert.GetRomanNumeral(decimalPlace, digit).ShouldBe(roman);
    }
}
