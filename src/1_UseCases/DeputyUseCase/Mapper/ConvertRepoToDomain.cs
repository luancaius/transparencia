using Entities.Entities;
using Repositories.DTO.NonRelational;

namespace DeputyUseCase.Mapper
{
    public class Mapper
    {
        public static DeputyDomain ConvertRepoToDomain(DeputyDetailRepo deputyDetailRepo)
        {

            var arrayName = deputyDetailRepo.NomeCivil.Split(" ");
            var firstName = arrayName[0];
            var lastName = arrayName[arrayName.Length - 1];
            var deputyDomain = DeputyDomain.CreateDeputy(
                deputyDetailRepo.IdDeputy.ToString(),
                firstName,
                lastName,
                deputyDetailRepo.NomeCivil,
                deputyDetailRepo.DataNascimento,
                deputyDetailRepo.UfNascimento,
                deputyDetailRepo.Cpf,
                deputyDetailRepo.Sexo,
                deputyDetailRepo.SiglaPartido,
                deputyDetailRepo.SiglaUf,
                deputyDetailRepo.NomeEleitoral,
                deputyDetailRepo.Email);

            return deputyDomain;
        }
    }
}
    }
}
