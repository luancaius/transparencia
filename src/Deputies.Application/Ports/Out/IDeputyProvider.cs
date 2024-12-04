using Deputies.Application.Dtos;

namespace Deputies.Application.Ports.Out
{
    public interface IDeputyProvider
    {
        Task<IEnumerable<DeputyListItemDto>> GetDeputiesListAsync(int legislatura);
        Task<DeputyDetailDto> GetDeputyDetailAsync(int deputyId);
    }
}