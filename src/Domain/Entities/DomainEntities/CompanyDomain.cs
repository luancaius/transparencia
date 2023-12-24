using Entities.ValueObject;

namespace Entities.DomainEntities;

public class CompanyDomain
{
    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    
    public static CompanyDomain CreateCompany(string name, string cnpj)
    {
        return new CompanyDomain(name, cnpj);
    }
    
    private CompanyDomain(string name, string cnpj)
    {
        Name = name;
        Cnpj = new Cnpj(cnpj);
    }
}