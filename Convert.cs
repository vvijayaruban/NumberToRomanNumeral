namespace NumberToRomanNumeral;

public class Convert
{
    private const int SupportedUpperLimit = 2000;

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

    public static string ToRomanNumeral(int number)
    {
        ValidateInput(number);
        if (RomanNumeralsDenominations.TryGetValue(number, out var value))
        {
            return value;
        }

        return "";
    }


    private static void ValidateInput(int number)
    {
        if (!IsNaturalNumber(number))
        {
            throw new InvalidDataException("Input should be a Natural Number to convert it to a Roman Numeral.");
        }

        if (number > SupportedUpperLimit)
        {
            throw new ArgumentOutOfRangeException(nameof(number),
                $"The supported upper limit of {nameof(ToRomanNumeral)} is {SupportedUpperLimit}.");
        }
    }

    private static bool IsNaturalNumber(int number)
    {
        return number > 0;
    }
}