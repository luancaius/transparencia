using Entities.DomainEntities;
using RelationalDatabase.DTO.Deputado;
using Services.DTO.Deputy;

namespace Services.Mapper;

public static class DeputyMapper
{
    public static Deputado MapToDeputado(DeputyDomain deputyDomain)
    {
        var deputado = new Deputado
        {
            IdApi = deputyDomain.Id,
            NomeEleitoral = deputyDomain.NomeEleitoral,
            NomeCivil = deputyDomain.Person.FirstName + " " + deputyDomain.Person.LastName,
            SiglaPartido = deputyDomain.Partido,
            Cpf = deputyDomain.Person.CPF.ToString(),
            DataNascimento = (DateTime)deputyDomain.Person.DateOfBirth!,
            UfNascimento = deputyDomain.Person.EstadoNascimento.ToString(),
            Sexo = deputyDomain.Person.Gender.ToString(),
            Email = deputyDomain.EmailDeputado.Value,
            UfRepresentacaoAtual = deputyDomain.EstadoRepresentacao.ToString(),
            DeputyExpenses = new List<DeputyExpense>()
        };
        
        return deputado;
    }
}