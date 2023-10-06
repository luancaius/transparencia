using Entity.API2_Soap.GetListaPresenca;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repository.Repositories.Mongo
{
    public class Api2DeputadoListaPresencaMongoRepository : IMongoRepository<DeputadoItemPresencaSoap>
    {
        private readonly IMongoDatabase _database;
        private string _tableName;

        public Api2DeputadoListaPresencaMongoRepository(MongoDbContext mongoContext, string tableName)
        {
            _database = mongoContext.Database;
            _tableName = tableName;

            Util.createUniqueIndexForTable<DeputadoItemPresencaSoap>(_database, tableName, new string[] {"NumMatriculaDeputado","Data"});
        }
        
        public IMongoCollection<DeputadoItemPresencaSoap> GetEntitiesCollection()
        {
            return _database.GetCollection<DeputadoItemPresencaSoap>(_tableName);
        }

        Task<DeputadoItemPresencaSoap> IMongoRepository<DeputadoItemPresencaSoap>.GetByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(DeputadoItemPresencaSoap entity)
        {
            throw new NotImplementedException();
        }

        public async Task InsertManyAsync(List<DeputadoItemPresencaSoap> entities)
        {
            try
            {
                var collection = GetEntitiesCollection();
                await collection.InsertManyAsync(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Mongo Repository:"+e.Message);
            }        
        }

        Task<List<DeputadoItemPresencaSoap>> IMongoRepository<DeputadoItemPresencaSoap>.GetAll()
        {
            throw new NotImplementedException();
        }

        IMongoCollection<DeputadoItemPresencaSoap> IMongoRepository<DeputadoItemPresencaSoap>.GetEntitiesCollection()
        {
            throw new NotImplementedException();
        }
    }
}