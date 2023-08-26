using Entity.API2_Soap;
using Entity.API2_Soap.GetAll;
using MongoDB.Bson;

namespace Repository.JsonEntity;

public class Api2DeputadoDtoMongo
{
    public ObjectId _id { get; set; }
    public string Nome { get; set; }
    public DeputadoSoap Dados { get; set; }
}