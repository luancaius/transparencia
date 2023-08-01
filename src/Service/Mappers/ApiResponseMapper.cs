
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Entity.Congresso;

namespace Service.Mappers;

public class ApiResponseMapper
{
    public async Task<T> MapResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            T data = await response.Content.ReadAsAsync<T>();
            return data;
        }

        throw new Exception($"Failed to call the API. Status code: {response.StatusCode}");
    }

    // Define methods to map specific API responses to your entity classes.
    public async Task<Deputado[]> MapDeputadosResponse(HttpResponseMessage response)
    {
        return await MapResponse<Deputado[]>(response);
    }

    public async Task<Partido[]> MapPartidosResponse(HttpResponseMessage response)
    {
        return await MapResponse<Partido[]>(response);
    }

    public async Task<Legislatura[]> MapLegislaturasResponse(HttpResponseMessage response)
    {
        return await MapResponse<Legislatura[]>(response);
    }
}
