using Entity.API1_Rest;
using Entity.Congresso;
using Newtonsoft.Json;

namespace Service.Mappers;

public static class Api1Mapper
{
    public static Api1DeputadoList MapApi1ListToDto(string deputadosRaw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoList>(deputadosRaw);

        return response;
    }
    
    public static Api1DeputadoDadosDto MapApi1ToDto(string deputadoRaw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoDadosDto>(deputadoRaw);

        return response;
    }

    public static List<Api1DeputadoDespesa> MapApi1DeputadoDespesas(string deputado_despesas_raw)
    {
        var response = JsonConvert.DeserializeObject<Api1DeputadoDespesaList>(deputado_despesas_raw);

        return response.DeputadoDespesaList;    
    }
}