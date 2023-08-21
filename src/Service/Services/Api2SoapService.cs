using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Entity.API2_Soap;
using Deputado = Entity.Congresso.Deputado;

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

                        XmlNode node = doc.DocumentElement.SelectSingleNode("soap:Envelope/soap:Body");
                        XmlSerializer serializerDeputado = new XmlSerializer(typeof(Deputado));
                        foreach(XmlNode child in node.ChildNodes)
                        {

                            var deputado = child.ChildNodes;

                        }
                        
                        // XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
                        // List<Deputado> deputados = new List<Deputado>();
                        // using (StringReader reader = new StringReader(soapResponse))
                        // {
                        //     Envelope envelope = (Envelope)serializer.Deserialize(reader);
                        //
                        //     // Access the parsed data like this:
                        //     var deputados2 = envelope?.Body?.ObterDeputadosResponse?.ObterDeputadosResult?.Deputados;
                        //
                        //
                        //     Console.WriteLine(deputados.Count);
                        //     // Now you can work with the list of Deputado objects.
                        // }
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
    }
}