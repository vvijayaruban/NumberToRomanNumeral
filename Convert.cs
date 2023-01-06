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


    public static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> IndividualDecimalPlaces =
        new Dictionary<string, IReadOnlyDictionary<int, string>>
        {
            {
                DecimalPlaces.Thousands, new Dictionary<int, string>
                {
                    {1, "M"},
                    {2, "MM"}
                }
            },
            {
                DecimalPlaces.Hundreds, new Dictionary<int, string>
                {
                    {1, "C"},
                    {2, "CC"},
                    {3, "CCC"},
                    {4, "CD"},
                    {5, "D"},
                    {6, "DC"},
                    {7, "DCC"},
                    {8, "DCCC"},
                    {9, "CM"}
                }
            }
            ,
            {
                DecimalPlaces.Tens, new Dictionary<int, string>
                {
                    {1, "X"},
                    {2, "XX"},
                    {3, "XXX"},
                    {4, "XL"},
                    {5, "L"},
                    {6, "DC"},
                    {7, "DCC"},
                    {8, "DCCC"},
                    {9, "CM"}
                }
            }
            ,
            {
                DecimalPlaces.Units, new Dictionary<int, string>
                {
                    {1, "I"},
                    {2, "II"},
                    {3, "III"},
                    {4, "IV"},
                    {5, "V"},
                    {6, "VI"},
                    {7, "VII"},
                    {8, "VIII"},
                    {9, "IX"}
                }
            }
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

    private static class DecimalPlaces
    {
        public const string Thousands = "Thousands", Hundreds = "Hundreds",Tens = "Tens",Units = "Units";
    }
}