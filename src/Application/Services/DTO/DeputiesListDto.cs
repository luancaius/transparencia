using Repositories.DTO;
using Repositories.DTO.NewApi;
using Repositories.DTO.NewApi.GetAll;
using Repositories.DTO.OldApi.GetAll;

namespace Services.DTO;

public class DeputiesListDto
{
    public List<DeputyDto> Deputies { get; set; } = new List<DeputyDto>();
    public DeputiesListDto(DeputiesListOldApi deputiesListOldApi, DeputiesListNewApi deputiesListNewApi)
    {
        for(int i=0;i<deputiesListOldApi.DeputiesOldApi.Count;i++)
        {
            var deputyNewApi = deputiesListNewApi.DeputiesNewApi[i];
            var deputyOldApi = deputiesListOldApi.DeputiesOldApi.First(a => a.Email == deputyNewApi.Email);
            var deputy = new DeputyDto(deputyNewApi, deputyOldApi);
            Deputies.Add(deputy);
        }
    }
}