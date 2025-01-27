using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public class Deputy : Person
{
    private Deputy(Cpf cpf, PersonName personName, string deputyName, string party, MultiSourceId multiSourceId)
        : base(cpf, personName)
    {
        if (string.IsNullOrWhiteSpace(party))
            throw new ArgumentException(nameof(party));
        DeputyName = deputyName;
        Party = party;
        MultiSourceId = multiSourceId ?? throw new ArgumentNullException(nameof(multiSourceId));
    }

    public string DeputyName { get; }    
    public string Party { get; }
    public MultiSourceId MultiSourceId { get; }

    public static Deputy Create(Cpf cpf, PersonName personName, string deputyName, string party, MultiSourceId multiSourceId)
    {
        if (cpf == null) 
            throw new ArgumentNullException(nameof(cpf));
        if (personName == null) 
            throw new ArgumentNullException(nameof(personName));
        if (string.IsNullOrWhiteSpace(party)) 
            throw new ArgumentException(nameof(party));
        if (multiSourceId == null) 
            throw new ArgumentNullException(nameof(multiSourceId));

        return new Deputy(cpf, personName, deputyName, party, multiSourceId);
    }

    public override string DisplayName => base.DisplayName;

    public override bool Equals(object obj)
    {
        if (obj is Deputy other)
        {
            return base.Equals(other)
                   && Party == other.Party
                   && MultiSourceId.Equals(other.MultiSourceId);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Party, MultiSourceId);
    }
}