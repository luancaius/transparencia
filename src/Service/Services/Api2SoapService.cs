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

        public async Task<List<Entity.Congresso.Deputado>> GetAllAPI2()
        {
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
                        var deputados = new DeputadosSoap();
                        XmlNode node = doc.DocumentElement.SelectSingleNode("//deputados");
                        XmlSerializer serializerDeputado = new XmlSerializer(typeof(DeputadosSoap));
                        using (XmlNodeReader reader = new XmlNodeReader(node))
                        {
                            deputados = (DeputadosSoap)serializerDeputado.Deserialize(reader);
                            Console.WriteLine($"Total deputados: {deputados.Deputado.Count}");
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

        private static T ConvertNode<T>(XmlNode node) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = (ser.Deserialize(stm) as T);

            return result;
        }
    }
}