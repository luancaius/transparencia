using Entities.Entities;
using Entities.ValueObject;
using Repositories.DTO.NonRelational;
using Repositories.DTO.Relational;

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
            Escolaridade = deputyDomain.Person.Escolaridade.ToString(),
            Legislatura = deputyDomain.Legislatura.Numero,
            GabineteInfo = ConvertGabineteVO(deputyDomain.Gabinete),
            DataNascimento = deputyDomain.Person.DateOfBirth.Value,
            UfNascimento = deputyDomain.Person.EstadoNascimento.ToString(),
            SiglaPartido = deputyDomain.Partido,
            SiglaUf = deputyDomain.EstadoRepresentacao.ToString(), 
            Email = deputyDomain.EmailDeputado.Value, 
            NomeEleitoral = deputyDomain.NomeEleitoral,
            RedeSocial = new List<string>(),
            
        };
    }

    private static DeputyDetailMongo.Gabinete ConvertGabineteVO(Gabinete gabinete)
    {
        return new DeputyDetailMongo.Gabinete()
        {
            Nome = gabinete.Nome,
            Predio = gabinete.Predio,
            Sala = gabinete.Sala,
            Andar = gabinete.Andar,
            Telefone = gabinete.Telefone,
            Email = gabinete.Email.Value
        };
    }

    public static DeputyDetailRepoRelational DeputyDetailRepoRelational(DeputyDomain deputyDomain)
    {
        var dto = new DeputyDetailRepoRelational
        {
            IdDeputy = int.Parse(deputyDomain.Id),
            NomeCivil = deputyDomain.Person.FullName,
            Cpf = deputyDomain.Person.CPF.ToString(), 
            Sexo = deputyDomain.Person.Gender.ToString(),
            DataNascimento = deputyDomain.Person.DateOfBirth.Value, 
            UfNascimento = deputyDomain.Person.EstadoNascimento.ToString(), 
            Nome = deputyDomain.NomeEleitoral,
            SiglaPartido = deputyDomain.Partido,
            SiglaUf = deputyDomain.EstadoRepresentacao.ToString(), 
            IdLegislatura = deputyDomain.Legislatura.Numero, 
            UrlFoto = deputyDomain.Photo.Url, 
            Email = deputyDomain.EmailDeputado.Value, 
            GabineteInfo = new DeputyDetailRepoRelational.Gabinete
            {
                Nome = deputyDomain.Gabinete.Nome,
                Predio = deputyDomain.Gabinete.Predio,
                Sala = deputyDomain.Gabinete.Sala,
                Andar = deputyDomain.Gabinete.Andar,
                Telefone = deputyDomain.Gabinete.Telefone,
                Email = deputyDomain.Gabinete.Email.Value 
            }
        };
        return dto;
    }
}
