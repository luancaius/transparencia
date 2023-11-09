using System.Text;
using ExternalAPI.Interfaces;
using Serilog;

namespace ExternalAPI.Implementation;

public class DadosAbertosOldApi : IDadosAbertosOldApi
{
    private IBaseApi _baseApi { get; set; }
    private readonly ILogger _logger;
    public DadosAbertosOldApi(IBaseApi baseApi, ILogger logger)
    {
        _baseApi = baseApi;
        _logger = logger.ForContext<DadosAbertosOldApi>();
    }
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
        _logger.Information("GetAllDeputiesRaw");
        string soapEndpoint = "https://www.camara.gov.br/SitCamaraWS/Deputados.asmx";

        string soapRequest = @"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:dep=""https://www.camara.gov.br/SitCamaraWS/Deputados"">
               <soapenv:Header/>
               <soapenv:Body>
                  <dep:ObterDeputados/>
               </soapenv:Body>
            </soapenv:Envelope>";

        try
        {
            StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            String response = await _baseApi.PostAsync(soapEndpoint, content);
            
            return response;
        }
        catch (Exception ex)
        {
            _logger.Error($"An error occurred: {ex.Message}");
        }

        return String.Empty;
    }

    public async Task<string> GetDeputyRaw(int legislatura, int id)
    {
        _logger.Information("GetDeputyRaw");
        string soapEndpoint = "https://www.camara.gov.br/SitCamaraWS/Deputados.asmx";

        string soapRequest = $@"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:dep=""https://www.camara.gov.br/SitCamaraWS/Deputados""> 
                    <soapenv:Header/>
                        <soapenv:Body>
                            <dep:ObterDetalhesDeputado>
                                <dep:ideCadastro>{id}</dep:ideCadastro>
                                <dep:numLegislatura>{legislatura}</dep:numLegislatura>
                            </dep:ObterDetalhesDeputado>
                        </soapenv:Body>
                </soapenv:Envelope>";

        try
        {
            StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

            String response = await _baseApi.PostAsync(soapEndpoint, content);
            
            return response;
        }
        catch (Exception ex)
        {
            _logger.Error($"An error occurred: {ex.Message}");
        }

        return String.Empty;
    }

    public async Task<string> GetDeputyWorkPresenceRaw(int year, int id)
    {
        throw new NotImplementedException();
    }
}