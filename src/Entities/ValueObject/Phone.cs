
namespace Entities.ValueObject;

public class Phone : ValueObject
{
    private string? CountryCode { get; set; }
    private string? AreaCode { get; set; }
    private string? Number { get; set; }

    public Phone(string? countryCode, string? areaCode, string? number)
    {
        CountryCode = countryCode;
        AreaCode = areaCode;
        Number = number;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return CountryCode;
        yield return AreaCode;
        yield return Number;
    }
}