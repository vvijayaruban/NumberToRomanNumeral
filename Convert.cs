using System.Reflection;
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

        var remainder = number;
        foreach (var decimalPlace in DecimalPlaces.PlaceValues
                     .OrderByDescending(x => x.Key)
                     .Select(y => y.Value))
        {
            (var quotient, remainder) = decimalPlace.Divide(remainder);

            if (quotient > 0) sb.Append(GetRomanNumeral(decimalPlace, quotient));

            if (remainder == 0) break;
        }

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

            public static implicit operator string(PlaceValue placeValue) => placeValue.Name;

            public (int Quotient, int Remainder) Divide(int dividend) => Value == 1 ? (dividend, 0) : (dividend / Value, dividend % Value);

            public static IEnumerable<KeyValuePair<int, PlaceValue>> GetAll()
            {
                var type = typeof(DecimalPlaces);
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (var info in fields)
                {
                    if (info.GetValue(null) is PlaceValue locatedValue)
                    {
                        yield return new KeyValuePair<int, PlaceValue>(locatedValue.Value, locatedValue);
                    }
                }
            }
        }

        public static readonly IDictionary<int, PlaceValue> PlaceValues = new Dictionary<int, PlaceValue>(PlaceValue.GetAll());
    }
}