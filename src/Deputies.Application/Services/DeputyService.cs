using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;

namespace Deputies.Application.Services
{
    public class DeputyService
    {
        private readonly IExternalDeputyService _externalDeputyService;

        public DeputyService(IExternalDeputyService externalDeputyService)
        {
            _externalDeputyService = externalDeputyService;
        }

        public async Task<List<Deputy>> GetDeputiesAsync(int year)
        {
            return await _externalDeputyService.GetAllDeputiesAsync(year);
        }
    }
}