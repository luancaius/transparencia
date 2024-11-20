using Deputies.Domain.Entities;

namespace Deputies.Application.Ports.Out
{
    public interface IExternalDeputyService
    {
        Task<List<Deputy>> GetAllDeputiesAsync(int year);
    }
}