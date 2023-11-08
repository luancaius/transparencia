using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;

namespace Services.DTO;

public class DeputiesListDto
{
    public List<DeputyDto> Deputies { get; }
    public DeputiesListDto(DeputiesListOldApi deputiesListOldApi, DeputiesListNewApi deputiesListNewApi)
    {
        int i = 0;
        Deputies = new List<DeputyDto>();
        try
        {
            for (i = 0; i < deputiesListOldApi.DeputiesOldApi.Count; i++)
            {
                var deputyNewApi = deputiesListNewApi.DeputiesNewApi[i];
                DeputyOldApi deputyOldApi = null;
                if (String.IsNullOrEmpty(deputyNewApi.Email))
                {
                    var listSimilarDeputies = deputiesListOldApi.DeputiesOldApi.FindAll(a => a.Uf == deputyNewApi.SiglaUf 
                        && a.Partido == deputyNewApi.SiglaPartido).ToList(); 
                    var firstName = deputyNewApi.Nome.Split(' ')[0].ToLowerInvariant();
                    var listSimilarName = deputiesListOldApi.DeputiesOldApi.FindAll(a => a.Nome.ToLowerInvariant().Contains(firstName)).ToList();
                    var deputyMissingInfo = new DeputyDto(deputyNewApi);
                    Deputies.Add(deputyMissingInfo);
                    continue;
                }

                deputyOldApi = deputiesListOldApi.DeputiesOldApi.First(a => a.Email == deputyNewApi.Email);
                var deputy = new DeputyDto(deputyNewApi, deputyOldApi);
                Deputies.Add(deputy);
            }
        } catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
}