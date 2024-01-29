using Entities.ValueObject;

namespace Entities.Entities;

public class SupplierDomain
{
    public string Name { get; private set; }
    public Cnpj? Cnpj { get; private set; }
    public Cpf? Cpf { get; private set; }
    
    private SupplierDomain(string name, Cnpj? cnpj, Cpf? cpf)
    {
        Name = name;
        Cnpj = cnpj;
        Cpf = cpf;
    }
    
    public static SupplierDomain CreateSupplier(string name, string cnpjOrCpf)
    {
        if (cnpjOrCpf.Length == 14)
            return new SupplierDomain(name, new Cnpj(cnpjOrCpf), null);
        return new SupplierDomain(name, null, new Cpf(cnpjOrCpf));
    }
}