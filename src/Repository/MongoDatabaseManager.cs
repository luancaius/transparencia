using MongoDB.Driver;
using Repository.JsonEntity;

namespace Repository;
public class MongoDatabaseManager
{
    private static readonly Lazy<MongoDatabaseManager> _instance = new Lazy<MongoDatabaseManager>(() => new MongoDatabaseManager());
        
    public static MongoDatabaseManager Instance => _instance.Value;

    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;
    private readonly string _tableName;

    private MongoDatabaseManager() // Private constructor to prevent external instantiation
    {
        // Here, you can set default values for connectionString, databaseName, and tableName
        string connectionString = "";
        string databaseName = "";
        _tableName = "";

        Initialize(connectionString, databaseName, _tableName);
    }

    private void Initialize(string connectionString, string databaseName, string tableName)
    {
        _mongoClient = new MongoClient(connectionString);
        _database = _mongoClient.GetDatabase(databaseName);
        _tableName = tableName;

        // Create a unique index on the "Nome" field
        var indexKeys = Builders<Api1DeputadoDtoMongo>.IndexKeys.Ascending("Nome");
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Api1DeputadoDtoMongo>(indexKeys, indexOptions);
        var collection = _database.GetCollection<Api1DeputadoDtoMongo>(tableName);
        collection.Indexes.CreateOne(indexModel);
    }

    public IMongoDatabase GetMongoDatabase()
    {
        return _database;
    }
}