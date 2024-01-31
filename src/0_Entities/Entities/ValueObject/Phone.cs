
namespace Entities.ValueObject;

public class Phone : ValueObject
{
    private string? CountryCode { get; }
    private string? AreaCode { get; }
    private string? Number { get; }

    public Phone(string? countryCode, string? areaCode, string? number)
    {
        CountryCode = countryCode;
        AreaCode = areaCode;
        Number = number;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return CountryCode;
        yield return AreaCode;
        yield return Number;
    }
}