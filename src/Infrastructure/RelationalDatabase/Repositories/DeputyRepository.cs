using System.Linq.Expressions;
using RelationalDatabase.DTO.Deputado;

namespace RelationalDatabase.Repositories;

public class DeputyRepository : IRepository<Deputado>
{
    public Deputado Get(Expression<Func<Deputado, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Deputado> GetAll(Expression<Func<Deputado, bool>> predicate, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public void Add(Deputado entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Deputado entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Deputado entity)
    {
        throw new NotImplementedException();
    }
}