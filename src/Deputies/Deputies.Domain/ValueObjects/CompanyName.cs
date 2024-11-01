namespace Deputies.Domain.ValueObjects;

public class CompanyName
{
    public string LegalName { get; }
    public string TradeName { get; }

    public CompanyName(string legalName, string? tradeName = null)
    {
        LegalName = legalName ?? throw new ArgumentNullException(nameof(legalName));
        TradeName = tradeName ?? legalName;
    }

    public override string ToString() => TradeName != LegalName ? $"{LegalName} ({TradeName})" : LegalName;
}