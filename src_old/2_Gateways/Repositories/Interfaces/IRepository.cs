using Repositories.DTO.NonRelational;
using Repositories.DTO.Relational;

namespace Repositories.Interfaces;

public interface IRepository
{
    Task SaveNonRelationalData(DeputyDetailMongo deputyDetail);
    Task SaveRelationalData(DeputyDetailRepoRelational deputyDetailRepoRelational);
    Task<List<DeputyDetailRepoRelational>> GetAllDeputies(int year);
}