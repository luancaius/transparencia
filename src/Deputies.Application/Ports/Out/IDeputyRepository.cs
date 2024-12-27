using Deputies.Domain.Entities;
using System.Threading.Tasks;

namespace Deputies.Application.Ports.Out
{
    public interface IDeputyRepository
    {
        Task SaveDeputyAsync(Deputy deputy);
    }
}