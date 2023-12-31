using Entities.ValueObject;

namespace Entities.DomainEntities;

public class CompanyDomain
{
    public string Name { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public Cpf Cpf { get; private set; }
    
    public static CompanyDomain CreateCompany(string name, string cnpjOrCpf)
    {
        if (cnpjOrCpf.Length == 14)
        {
            return new CompanyDomain(name, new Cnpj(cnpjOrCpf), null);
        }

        if (cnpjOrCpf.Length == 11)
        {
            return new CompanyDomain(name, null, new Cpf(cnpjOrCpf));
        }
        throw new ArgumentException("Invalid CNPJ or CPF length.");
    }
    
    private CompanyDomain(string name, Cnpj cnpj, Cpf cpf)
    {
        Name = name;
        Cnpj = cnpj;
        Cpf = cpf;
    }
}