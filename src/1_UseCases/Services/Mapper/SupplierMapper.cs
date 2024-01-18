using Entities.DomainEntities;
using RelationalDatabase.DTO;

namespace Services.Mapper;

public static class SupplierMapper
{
    public static Supplier MapToEntity(SupplierDomain supplierDomain)
    {
        var supplier = new Supplier
        {
            Name = supplierDomain.Name,
            Cnpj = supplierDomain.Cnpj?.ToString(),
            Cpf = supplierDomain.Cpf?.ToString()
        };

        return supplier;
    }
}