using Entity.API1;
using MongoDB.Bson;

namespace Repository.JsonEntity;

public class Api1DeputadoDtoMongo
{
    public ObjectId _id { get; set; }
    public string Nome { get; set; }
    public Api1DeputadoDto Dados { get; set; }
}