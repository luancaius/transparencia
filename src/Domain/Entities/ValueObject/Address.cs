namespace Entities.ValueObject;

public class Address : ValueObject
{
    public string? Street { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? ZipCode { get; private set; }

    private Address() { }

    public Address(string? street, string? city, string? state, string? country, string? zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
    
    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {Country}, {ZipCode}";
    }
}