using Entities.Entities;
using Entities.ValueObject;
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

    private static DeputyDetailMongo.Gabinete ConvertGabineteVO(GabineteVO gabineteVo)
    {
        return new DeputyDetailMongo.Gabinete()
        {
            Nome = gabineteVo.Nome,
            Predio = gabineteVo.Predio,
            Sala = gabineteVo.Sala,
            Andar = gabineteVo.Andar,
            Telefone = gabineteVo.Telefone,
            Email = gabineteVo.Email.Value
        };
    }
}
