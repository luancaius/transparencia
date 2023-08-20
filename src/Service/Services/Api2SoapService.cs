using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Entity.Congresso;

namespace Service.Services
{
    public class Api2SoapService
    {
        private readonly HttpClient _httpClient;

        public Api2SoapService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Entity.Congresso.Deputado>> GetAllAPI2()
        {
            string soapEndpoint = "https://www.camara.leg.br/SitCamaraWS/Deputados.asmx"; // Replace with the actual URL

            string soapRequest = @"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:dep=""https://www.camara.gov.br/SitCamaraWS/Deputados"">
   <soapenv:Header/>
   <soapenv:Body>
      <dep:ObterDeputados/>
   </soapenv:Body>
</soapenv:Envelope>";

            try
            {
                // Create the SOAP request
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, soapEndpoint);
                request.Content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

                // Set SOAP action header
                request.Headers.Add("SOAPAction", "https://www.camara.gov.br/SitCamaraWS/Deputados/ObterDeputados");

                // Send the request and get the response
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string soapResponse = await response.Content.ReadAsStringAsync();

                    XmlSerializer serializer = new XmlSerializer(typeof(Entity.API2_Soap.Envelope));
                    List<Entity.Congresso.Deputado> deputados = new List<Deputado>();
                    using (StringReader reader = new StringReader(soapResponse))
                    {
                        Entity.API2_Soap.Envelope envelope = (Entity.API2_Soap.Envelope)serializer.Deserialize(reader);

                        // Access the parsed data like this:
                        var deputados2 = envelope?.Body?.ObterDeputadosResponse?.ObterDeputadosResult?.Deputados;
                        Console.WriteLine(deputados2.Count);
                        // Now you can work with the list of Deputado objects.
                    }

                    return deputados;
                }
                else
                {
                    Console.WriteLine($"SOAP request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }
}
