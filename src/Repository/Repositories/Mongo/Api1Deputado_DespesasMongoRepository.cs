using Entity.API1_Rest;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repository.Repositories.Mongo
{
    public class Api1Deputado_DespesasMongoRepository : IMongoRepository<Api1DeputadoDespesa>
    {
        private readonly IMongoDatabase _database;
        private string _tableName;

        public Api1Deputado_DespesasMongoRepository(MongoDbContext mongoContext, string tableName)
        {
            _database = mongoContext.Database;
            _tableName = tableName;

            Util.createUniqueIndexForTable<Api1DeputadoDespesa>(_database, tableName, new string[] {"DataDocumento","CnpjCpfFornecedor","NumDocumento"});
        }
        
        public IMongoCollection<Api1DeputadoDespesa> GetEntitiesCollection()
        {
            return _database.GetCollection<Api1DeputadoDespesa>(_tableName);
        }

        public Task<Api1DeputadoDespesa> GetByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Api1DeputadoDespesa entity)
        {
            var collection = GetEntitiesCollection();
            await collection.InsertOneAsync(entity);
        }

        public async Task InsertManyAsync(List<Api1DeputadoDespesa> entities)
        {
            var collection = GetEntitiesCollection();
            await collection.InsertManyAsync(entities);
        }

        public Task<List<Api1DeputadoDespesa>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}