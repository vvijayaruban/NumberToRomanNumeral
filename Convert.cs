using System.Text;

namespace NumberToRomanNumeral;

public class Convert
{
    private const int SupportedUpperLimit = 2000;
    
    private static readonly IReadOnlyDictionary<string, IReadOnlyDictionary<int, string>> DecimalPlacesMap =
        new Dictionary<string, IReadOnlyDictionary<int, string>>
        {
            {
                DecimalPlaces.Thousand, new Dictionary<int, string>
                {
                    {1, "M"},
                    {2, "MM"}
                }
            },
            {
                DecimalPlaces.Hundred, new Dictionary<int, string>
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
            },
            {
                DecimalPlaces.Ten, new Dictionary<int, string>
                {
                    {1, "X"},
                    {2, "XX"},
                    {3, "XXX"},
                    {4, "XL"},
                    {5, "L"},
                    {6, "LX"},
                    {7, "LXX"},
                    {8, "LXXX"},
                    {9, "XC"}
                }
            },
            {
                DecimalPlaces.Unit, new Dictionary<int, string>
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

        var thousand = DecimalPlaces.Thousand;

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

        var hundred = DecimalPlaces.Hundred;

        quotient = remainder / hundred.Value;
        remainder %= hundred.Value;

        if (quotient > 0)
        {
            sb.Append(GetRomanNumeral(hundred, quotient));
        }

        if (remainder == 0)
        {
            return sb.ToString();
        }

        var ten = DecimalPlaces.Ten;
        quotient = remainder / ten.Value;
        remainder %= ten.Value;

        if (quotient > 0)
        {
            sb.Append(GetRomanNumeral(ten, quotient));
        }

        if (remainder == 0)
        {
            return sb.ToString();
        }

        sb.Append(GetRomanNumeral(DecimalPlaces.Unit, remainder));

        return sb.ToString();
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
        public static readonly PlaceValue Thousand = new("Thousands", 1000);
        public static readonly PlaceValue Hundred = new("Hundreds", 100);
        public static readonly PlaceValue Ten = new("Tens", 10);
        public static readonly PlaceValue Unit = new("Units", 1);
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