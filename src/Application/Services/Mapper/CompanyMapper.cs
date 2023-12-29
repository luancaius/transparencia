using Entities.DomainEntities;
using RelationalDatabase.DTO;

namespace Services.Mapper;

public static class CompanyMapper
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
}