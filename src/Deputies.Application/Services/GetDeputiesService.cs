using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;

namespace Deputies.Application.Services
{
    public class GetDeputiesService : IGetDeputiesUseCase
    {
        private readonly IDeputyProvider _deputyProvider;

        public GetDeputiesService(IDeputyProvider deputyProvider)
        {
            _deputyProvider = deputyProvider;
        }

        public async Task<IEnumerable<DeputyResponse>> GetDeputiesAsync(int year)
        {
            var deputies = await _deputyProvider.GetDeputiesAsync(year);
            
            return deputies.Select(d => new DeputyResponse(
                d.MultiSourceId.ToString(),
                d.DeputyName,
                d.Party,
                ""));  // Add state if needed
        }
    }
}