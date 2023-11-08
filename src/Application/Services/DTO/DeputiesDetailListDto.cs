using Repositories.DTO.OldApi.GetById;

namespace Services.DTO;

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
                var deputyDetailNewApi = deputiesDetailListNewApi.First(a => a.Email == deputyDetailOldApi.Email);
                var deputyDetail = new DeputyDetailDto(deputyDetailOldApi, deputyDetailNewApi);
                DeputiesDetail.Add(deputyDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{deputyDetailOldApi.Id}-{e}");
                throw;
            }
        }
    }
}