using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Entity.API2_Soap;

namespace Service.Services
{
    public class Api2SoapService
    {
        private readonly HttpClient _httpClient;

        public Api2SoapService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DeputadoSoap>> GetAllAPI2()
        {
            var deputados = new List<DeputadoSoap>();

            string soapEndpoint = "https://www.camara.gov.br/SitCamaraWS/Deputados.asmx";

            string soapRequest = @"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:dep=""https://www.camara.gov.br/SitCamaraWS/Deputados"">
   <soapenv:Header/>
   <soapenv:Body>
      <dep:ObterDeputados/>
   </soapenv:Body>
</soapenv:Envelope>";
            string soapAction = "https://www.camara.gov.br/SitCamaraWS/Deputados/ObterDeputados";

            XmlNode currentNode = null;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("SOAPAction", soapAction);
                    StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

                    HttpResponseMessage response = await httpClient.PostAsync(soapEndpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string soapResponse = await response.Content.ReadAsStringAsync();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(soapResponse);
                        XmlNodeList nodes = doc.DocumentElement.SelectNodes("//deputados/deputado");
                        XmlSerializer serializerDeputado = new XmlSerializer(typeof(DeputadoSoap));
                        foreach (XmlNode node in nodes)
                        {
                            using (XmlNodeReader reader = new XmlNodeReader(node))
                            {
                                var deputado = (DeputadoSoap)serializerDeputado.Deserialize(reader);
                                deputados.Add(deputado);
                            }
                        }
                        
                        return deputados;
                    }
                    else
                    {
                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        public async Task<DeputadoSoap> GetDeputadoById(int id)
        {
            return new DeputadoSoap();
        }
    }
}