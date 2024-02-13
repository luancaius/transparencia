using Entities.Entities;
using Repositories.DTO.NonRelational;

namespace DeputyUseCase.Mapper;

public static class ConvertFromDomain
{
    public static DeputyDetailMongo DeputyDetailRepo(DeputyDomain deputyDomain)
    {
        return new DeputyDetailMongo
        {
            Id = int.Parse(deputyDomain.Id), 
            NomeCivil = deputyDomain.Person.FullName,
            Cpf = deputyDomain.Person.CPF.ToString(),
            Sexo = deputyDomain.Person.Gender.ToString(),
            DataNascimento = deputyDomain.Person.DateOfBirth.Value,
            UfNascimento = deputyDomain.Person.EstadoNascimento.ToString(),
            SiglaPartido = deputyDomain.Partido,
            SiglaUf = deputyDomain.EstadoRepresentacao.ToString(), 
            Email = deputyDomain.EmailDeputado.Value, 
            NomeEleitoral = deputyDomain.NomeEleitoral,
            RedeSocial = new List<string>() 
        };
    }
}
