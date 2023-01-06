namespace NumberToRomanNumeral;

using System.Reflection;
using System.Text;

public class TestDataProvider
{
    public static readonly string DataForIndividualDecimalPlaces = ReadTestDataFile("Data_for_Individual_decimal_places.txt");

    public static readonly string
        DataTestingNumbersContainingTwoOrMoreDecimalDigitsIsBuiltByAppendingTheRomanNumeralEquivalentForEachFromHighestToLowest =
            ReadTestDataFile(
                "Data_testing_numbers_containing_two_or_more_decimal_digits_is_built_by_appending_the_Roman_numeral_equivalent_for_each_from_highest_to_lowest.txt");

    private static string ReadTestDataFile(string fileName)
    {
        var path = $"{typeof(TestDataProvider).Namespace}.TestData.{fileName}";
        var assembly = Assembly.GetExecutingAssembly();

        using var resourceStream = assembly.GetManifestResourceStream(path);
        using var streamReader = new StreamReader(resourceStream ?? throw new FileNotFoundException($"{fileName} : {path} missing."), Encoding.UTF8);
        return streamReader.ReadToEnd();
    }
}