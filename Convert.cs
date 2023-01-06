namespace NumberToRomanNumeral;

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

    //public static Stack<int> OrderDenominationsStacks { get; } =
    //    new(RomanNumeralsDenominations.OrderBy(x => x.Key).Select(x => x.Key));

    public static string ToRomanNumeral(int number)
    {
        if (number <= 0)
            throw new InvalidDataException("Input should be a Natural Number to convert it to a Roman Numeral.");
        if (RomanNumeralsDenominations.TryGetValue(number, out var value))
        {
            return value;
        }

        //var pop = OrderDenominationsStacks.Pop();
        //if (number > pop)
        //{
        //    var quotient = number / pop;
        //    var remainder = number % pop;

        //}

        return "";
    }
}