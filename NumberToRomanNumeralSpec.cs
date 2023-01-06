using Shouldly;
using Xunit;

namespace NumberToRomanNumeral;

/*
 * Write code to convert any number between 1 and 2000 to a Roman Numeral, using the standard rules.
 * You can use libraries/packages within reason but your solution must demonstrate the application of the rules logic you have written.
 * Your solution will be assessed on whether it: -
 *
    1) solves the problem
    2) is well structured
    3) has an appropriate level of complexity
    Feel free to use tools you are comfortable with, but you must use C# as the main language. When you are done provide access to the solution so that the interview panel may assess it in its entirety.
    Note any code you develop is your code. It will not be copied or reused / repurposed within XPS.  
 */

public class Convert
{
    public static readonly IReadOnlyDictionary<int, string> RomanNumeralsDenominations = new Dictionary<int, string>
    {
        {1, "I"},
        {5, "V"},
        {10, "X"},
        {50, "L"},
        {100, "C"},
        {500, "D"},
        {1000, "M"}
    };

    public static Stack<int> OrderDenominationsStacks { get; } =
        new(RomanNumeralsDenominations.OrderBy(x => x.Key).Select(x => x.Key));

    public static string ToRomanNumeral(int number)
    {
        if (number <= 0)
            throw new InvalidDataException("Input should be a Natural Number to convert it to a Roman Numeral.");
        if (RomanNumeralsDenominations.TryGetValue(number, out var value))
        {
            return value;
        }

        var pop = OrderDenominationsStacks.Pop();
        if (number > pop)
        {
            var quotient = number / pop;
            var remainder = number % pop;

        }

        return "";
    }
}

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
}
