using Entities.Entities;
using Gateways.DTO;
using Repositories.DTO.NonRelational;

namespace DeputyUseCase.Mapper;

public static class ConvertToDomain
{
    public static DeputyDomain Deputy(DeputyDetailInfo deputyDetailInfo)
    {
        var arrayName = deputyDetailInfo.NomeCivil.Split(" ");
        var firstName = arrayName[0];
        var lastName = arrayName[arrayName.Length - 1];
        var deputyDomain = DeputyDomain.CreateDeputy(
            deputyDetailInfo.IdDeputy.ToString(),
            firstName,
            lastName,
            deputyDetailInfo.NomeCivil,
            deputyDetailInfo.DataNascimento,
            deputyDetailInfo.UfNascimento,
            deputyDetailInfo.Cpf,
            deputyDetailInfo.Sexo,
            deputyDetailInfo.SiglaPartido,
            deputyDetailInfo.SiglaUf,
            deputyDetailInfo.NomeEleitoral,
            deputyDetailInfo.Email);

        return deputyDomain;
    }
}