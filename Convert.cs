using System.Text;

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


    public static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> DecimalPlacesMap =
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
        var sb = new StringBuilder();

        int quotient, remainder = number;

        var thousand = DecimalPlaces.Thousands;

        quotient = remainder / thousand.Value;
        remainder %= thousand.Value;

        if (quotient > 0)
        {
            sb.Append(GetRomanNumeral(thousand, quotient));
        }

        if (remainder == 0)
        {
            return sb.ToString();
        }
        

        return "";
    }

    public static string GetRomanNumeral(string forDecimalPlace, int position)
    {
        return DecimalPlacesMap[forDecimalPlace][position];
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

    public static class DecimalPlaces
    {
        public static readonly PlaceValue Thousands = new("Thousands", 1000);
        public static readonly PlaceValue Hundreds = new("Hundreds", 100);
        public static readonly PlaceValue Tens = new("Tens", 10);
        public static readonly PlaceValue Units = new("Units", 1);

        public class PlaceValue
        {
            public PlaceValue(string name, int value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public int Value { get; }

            public static implicit operator string(PlaceValue placeValue)
            {
                return placeValue.Name;
            }
        }
    }
}