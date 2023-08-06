using Entity.Congresso;
using Newtonsoft.Json;
using Service.DTO.API1;

namespace Service.Mappers;

public static class Api1Mapper
{
    public static Api1DeputadoList mapApi1ListToDto(string deputadosRaw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoList>(deputadosRaw);

        return response;
    }
    
    public static Api1DeputadoDadosDto mapApi1ToDto(string deputadoRaw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoDadosDto>(deputadoRaw);

        return response;
    }

    public static List<Deputado> mapListApi1ToEntity(Api1DeputadoList source)
    {
        var destin = new List<Deputado>();
        foreach (var item in source.DeputadoList)
        {
            var deputado = new Deputado();
            deputado.Nome = item.Nome;
            deputado.Id = item.Id;
            destin.Add(deputado);
        }
        
        return destin;
    }
    
    public static Deputado mapApi1ToEntity(Api1DeputadoDadosDto source)
    {
        var destin = new Deputado();
        destin.Nome = source.DeputadoApi1.NomeCivil;
        
        return destin;
    }
}