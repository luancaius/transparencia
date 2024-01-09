using Entities.DomainEntities;
using RelationalDatabase.DTO;

namespace Services.Mapper;

public static class Mapper
{
    public static Company MapToCompany(CompanyDomain companyDomain)
    {
        var company = new Company
        {
            Cnpj = companyDomain.Cnpj.ToString(),
            Name = companyDomain.Name
        };

        return company;
    }
    
    public static Person MapToPerson(PersonDomain personDomain)
    {
        var person = new Person
        {
            Cpf = personDomain.CPF.ToString(),
            Name = personDomain.FullName
        };

        return person;
    }
}