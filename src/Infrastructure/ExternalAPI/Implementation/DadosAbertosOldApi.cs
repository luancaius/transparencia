using System.Text;
using ExternalAPI.Interfaces;

namespace ExternalAPI.Implementation;

public class DadosAbertosOldApi : IDadosAbertosOldApi
{
    private IBaseApi _baseApi { get; set; }
    public DadosAbertosOldApi(IBaseApi baseApi)
    {
        _baseApi = baseApi;
    }
    public async Task<string> GetAllDeputiesRaw(int legislatura)
    {
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
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return String.Empty;
    }

    public async Task<string> GetDeputyRaw(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetDeputyWorkPresenceRaw(int year, int id)
    {
        throw new NotImplementedException();
    }
}