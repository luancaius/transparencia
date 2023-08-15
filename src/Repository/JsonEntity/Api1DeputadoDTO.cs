using Entity.API1;
using MongoDB.Bson;

namespace Repository.JsonEntity;

public class Api1DeputadoDtoMongo
{
    public ObjectId ObjectId { get; set; }
    public Api1DeputadoDto Api1DeputadoDto { get; set; }
}