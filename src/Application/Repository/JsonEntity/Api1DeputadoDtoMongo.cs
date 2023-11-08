using Entity.API1_Rest;
using MongoDB.Bson;

namespace Repository.JsonEntity;

public class Api1DeputadoDtoMongo
{
    public ObjectId _id { get; }
    public string Nome { get; set; }
    public Api1DeputadoDto Dados { get; set; }
}