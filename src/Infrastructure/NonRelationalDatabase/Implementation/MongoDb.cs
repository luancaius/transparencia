using NonRelationalDatabase.Interfaces;

namespace NonRelationalDatabase.Implementation;

public class MongoDb : INonRelationalDatabase
{
    public Task Insert<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public Task InsertMany<T>(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task<T> Get<T>(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll<T>()
    {
        throw new NotImplementedException();
    }
}