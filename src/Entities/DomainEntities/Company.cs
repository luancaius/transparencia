using Entities.ValueObject;

namespace Entities.DomainEntities;

public class Company
{
    public Company(Guid id, string name, string industry, Address headquartersAddress, Email contactEmail, Phone contactPhone, string cnpj)
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
    public Email ContactEmail { get; private set; }
    public Phone ContactPhone { get; private set; }
    public string CNPJ { get; private set; }

    // Constructors, methods, and any additional logic here
}