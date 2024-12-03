using Deputies.Domain.Entities;

namespace Deputies.Application.Ports.Out;

public interface IDeputyProvider
{
    Task<IEnumerable<Deputy>> GetDeputiesAsync(int year);
}