using Repositories.DTO.NonRelational;

namespace Repositories.Interfaces;

public interface IRepository
{
    Task SaveNonRelationalData(DeputyDetailRepo deputyDetail);
}