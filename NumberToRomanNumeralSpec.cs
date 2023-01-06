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
        public static string ToRomanNumeral(int number)
        {
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
}
