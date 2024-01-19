using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Repositories.DTO;

public class BaseEntityDTO
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    // TODO delete in the future
    public string IdEntity { get; set; }
}