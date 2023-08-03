using Entity.Congresso;

namespace Service;

public class DeputadoService : ApiService
{
    private String baseUrl = "https://dadosabertos.camara.leg.br/api/v2";

    public async Task GetLatestDeputados()
    {
        var deputados = await GetAll();
        foreach (var deputado_item in deputados)
        {
            var deputado = await GetById(deputado_item.Id);
            Console.WriteLine(deputado);
        }
    }
    
    private async Task<List<Deputado>> GetAll()
    {
        String apiUrl = "/deputados";
        String param = "?ordem=ASC&ordenarPor=nome";

        String deputados_raw = await GetAsync(baseUrl+apiUrl+param);

        List<Deputado> deputados = DeputadoMapper.map(deputados_raw);

        return deputados;
    }
    
    private async Task<Deputado> GetById(int Id)
    {
        String apiUrl = $"/deputados/{Id}";

        String deputado_raw = await GetAsync(baseUrl+apiUrl);

        Deputado deputado = DeputadoMapper.mapById(deputado_raw);

        return deputado;
    }
}