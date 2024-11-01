using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public class Company
{
    public Cnpj Cnpj { get; set; }
    public CompanyName CompanyName { get; set; }
}