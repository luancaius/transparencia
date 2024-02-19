using Entities.Entities;
using Entities.ValueObject;
using Gateways.DTO;

namespace DeputyUseCase.Mapper;

public static class ConvertToDomain
{
    public static DeputyDomain Deputy(DeputyDetailInfo deputyDetailInfo)
    {
        var arrayName = deputyDetailInfo.NomeCivil.Split(" ");
        var firstName = arrayName[0];
        var lastName = arrayName[arrayName.Length - 1];
        var gabinete = GabineteVO.CreateGabinete(
            deputyDetailInfo.GabineteInfo.Nome,
            deputyDetailInfo.GabineteInfo.Predio,
            deputyDetailInfo.GabineteInfo.Sala,
            deputyDetailInfo.GabineteInfo.Andar,
            deputyDetailInfo.GabineteInfo.Telefone,
            deputyDetailInfo.GabineteInfo.Email);
        
        var person = PersonDomain.CreatePerson(
            firstName,
            lastName,
            deputyDetailInfo.NomeCivil,
            deputyDetailInfo.DataNascimento,
            "",
            deputyDetailInfo.UfNascimento,
            deputyDetailInfo.Cpf,
            deputyDetailInfo.Sexo,
            deputyDetailInfo.Escolaridade
            );
        var deputyDomain = DeputyDomain.CreateDeputy(
            deputyDetailInfo.IdDeputy.ToString(),
            person,
            deputyDetailInfo.SiglaPartido,
            deputyDetailInfo.SiglaUf,
            deputyDetailInfo.NomeEleitoral,
            deputyDetailInfo.Email,
            deputyDetailInfo.UrlFoto,
            deputyDetailInfo.IdLegislatura,
            gabinete
            );

        return deputyDomain;
    }
}