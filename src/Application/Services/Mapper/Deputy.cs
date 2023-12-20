using Entities.DomainEntities;
using RelationalDatabase.DTO.Deputado;

namespace Services.Mapper;

public static class DeputyMapper
{
    public static Deputado MapToDeputado(DeputyDomain deputyDomain)
    {
        var deputado = new Deputado
        {
            Nome = deputyDomain.Person.FullName,
            NomeEleitoral = deputyDomain.NomeEleitoral,
            NomeCivil = deputyDomain.Person.FirstName + " " + deputyDomain.Person.LastName,
            SiglaPartido = deputyDomain.Partido,
            SiglaUf = deputyDomain.UfRepresentacao,
            Cpf = deputyDomain.Person.CPF.ToString(),
            DataNascimento = deputyDomain.Person.DateOfBirth,
            UfNascimento = deputyDomain.Person.StateBirth,
            Sexo = deputyDomain.Person.Gender.ToString(),
            Email = deputyDomain.EmailDeputado.Value
        };
        
        return deputado;
    }
}