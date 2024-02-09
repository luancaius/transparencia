using Repositories.DTO.NonRelational;
using Repositories.DTO.Relational;

namespace Repositories.Interfaces;

public interface IRepository
{
    Task SaveNonRelationalData(DeputyDetailRepo deputyDetail);
    Task SaveRelationalData(DeputyDetailRepoRelational deputyDetailRepoRelational);
}