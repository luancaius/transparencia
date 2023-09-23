namespace Entities.ValueObject;


public class Email : ValueObject
{
    public string? Address { get; private set; }

    private Email() { }

    public Email(string? address)
    {
        Address = address;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Address;
    }
}

