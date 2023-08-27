using Entity.API2_Soap;
using Entity.API2_Soap.GetAll;
using Entity.API2_Soap.GetById;
using MongoDB.Bson;

namespace Repository.JsonEntity;

public class Api2DeputadoDtoMongo
{
    public ObjectId _id { get; set; }
    public string Nome { get; set; }
    public DeputadoByIdSoap Dados { get; set; }
}