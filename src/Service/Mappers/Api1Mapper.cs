using Entity.Congresso;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.DTO.API1;

namespace Service;

public static class Api1Mapper
{
    public static Api1DeputadoList map(string deputadosRaw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoList>(deputadosRaw);

        return response;
    }
    
    public static Api1DeputadoDto mapById(string deputadoRaw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoDto>(deputadoRaw);

        return response;
    }

    public static List<Deputado> mapList(Api1DeputadoList source)
    {
        var destin = new List<Deputado>();
        foreach (var item in source.DeputadoList)
        {
            var deputado = new Deputado();
            deputado.Nome = item.Nome;
            destin.Add(deputado);
        }
        
        return destin;
    }
}