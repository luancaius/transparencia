using Repositories.DTO.NewApi.GetById;
using Repositories.DTO.OldApi.GetById;

namespace Services.DTO.Deputy;

public class DeputiesDetailListDto
{
    public List<DeputyDetailDto> DeputiesDetail { get; } = new List<DeputyDetailDto>();
    public DeputiesDetailListDto(List<DeputyDetailOldApi> deputiesDetailListOldApi, List<DeputyDetailNewApi> deputiesDetailListNewApi)
    {
        for(int i=0;i<deputiesDetailListOldApi.Count;i++)
        {
            var deputyDetailOldApi = deputiesDetailListOldApi[i];
            try
            {
                var deputyDetailNewApi = deputiesDetailListNewApi.FirstOrDefault(a => a.Email == deputyDetailOldApi.Email) ??
                                         deputiesDetailListNewApi.First(a => a.Nome == deputyDetailOldApi.NomeParlamentarAtual);
                var deputyDetail = new DeputyDetailDto(deputyDetailNewApi, deputyDetailOldApi);
                DeputiesDetail.Add(deputyDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{deputyDetailOldApi.Id}-{e}");
                throw;
            }
        }

        var counter = 0;
        foreach (var deputyDetailNewApi in deputiesDetailListNewApi)
        {
            if (DeputiesDetail.Any(a => a.IdDeputy == deputyDetailNewApi.IdDeputy)) continue;
            var deputyDetail = new DeputyDetailDto(deputyDetailNewApi);
            DeputiesDetail.Add(deputyDetail);
            counter++;
        }
        Console.WriteLine("Total deputados extras na new api: " + counter);
        
    }
}