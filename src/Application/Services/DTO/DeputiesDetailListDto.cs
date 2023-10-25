using Repositories.DTO;
using Repositories.DTO.NewApi;
using Repositories.DTO.NewApi.GetById;
using Repositories.DTO.OldApi.GetById;

namespace Services.DTO;

public class DeputiesDetailListDto
{
    public List<DeputyDetailDto> DeputiesDetail { get; set; } = new List<DeputyDetailDto>();
}