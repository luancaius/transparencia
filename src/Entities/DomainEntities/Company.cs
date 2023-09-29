using Entities.ValueObject;

namespace Entities.DomainEntities;

public class Company
{
    public Company(Guid id, string name, string industry, Address headquartersAddress, String contactEmail, Phone contactPhone, Cnpj cnpj)
    {
        Id = id;
        Name = name;
        Industry = industry;
        HeadquartersAddress = headquartersAddress;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        CNPJ = cnpj;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Industry { get; private set; }
    public Address HeadquartersAddress { get; private set; }
    public String ContactEmail { get; private set; }
    public Phone ContactPhone { get; private set; }
    public Cnpj CNPJ { get; private set; }
}