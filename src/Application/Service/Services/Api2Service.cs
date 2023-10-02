using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Entity.API2_Soap.GetAll;
using Entity.API2_Soap.GetById;
using Entity.API2_Soap.GetListaPresenca;

namespace Service.Services
{
    public class Api2Service
    {
        private RestService _restService;

        public Api2Service(RestService restService)
        {
            _restService = restService;
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

            DeputadoSoap deputado = null;
            try
            {
                StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

                String response = await _restService.PostAsync(soapEndpoint, content);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("//deputados/deputado");
                XmlSerializer serializerDeputado = new XmlSerializer(typeof(DeputadoSoap));
                foreach (XmlNode node in nodes)
                {
                    using (XmlNodeReader reader = new XmlNodeReader(node))
                    {
                        deputado = (DeputadoSoap)serializerDeputado.Deserialize(reader);
                        deputados.Add(deputado);
                    }
                }

                return deputados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred near {deputado.Nome}: {ex.Message}");
            }

            return null;
        }

        public async Task<DeputadoByIdSoap> GetDeputadoById(int id, int numLegislatura)
        {
            string soapEndpoint = "https://www.camara.gov.br/SitCamaraWS/Deputados.asmx";

            string soapRequest = $@"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:dep=""https://www.camara.gov.br/SitCamaraWS/Deputados""> 
                    <soapenv:Header/>
                        <soapenv:Body>
                            <dep:ObterDetalhesDeputado>
                                <dep:ideCadastro>{id}</dep:ideCadastro>
                                <dep:numLegislatura>{numLegislatura}</dep:numLegislatura>
                            </dep:ObterDetalhesDeputado>
                        </soapenv:Body>
                </soapenv:Envelope>";

            DeputadoByIdSoap deputado = null;
            try
            {
                StringContent content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

                String response = await _restService.PostAsync(soapEndpoint, content);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlNode node = doc.DocumentElement.SelectSingleNode("//Deputados/Deputado");
                XmlSerializer serializerDeputado = new XmlSerializer(typeof(DeputadoByIdSoap));

                using (XmlNodeReader reader = new XmlNodeReader(node))
                {
                    deputado = (DeputadoByIdSoap)serializerDeputado.Deserialize(reader);
                    Console.WriteLine(deputado);
                }

                return deputado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        public async Task<DeputadoListaPresencaSoap> GetDeputadoListaPresenca(DateTime dateBegin, DateTime dateEnd,
            int numMatricula)
        {
            DeputadoListaPresencaSoap deputadoListaPresenca = null;
            try
            {
                String dateInicioStr = dateBegin.ToString("dd/MM/yyyy");
                String dateFimStr = dateEnd.ToString("dd/MM/yyyy");
                String url = $"https://www.camara.gov.br/SitCamaraWS/sessoesreunioes.asmx/ListarPresencasParlamentar?" +
                             $"dataIni={dateInicioStr}&dataFim={dateFimStr}&numMatriculaParlamentar={numMatricula}";

                String response = await _restService.GetAsync(url);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlNode node = doc.DocumentElement;
                XmlSerializer serializerDeputado = new XmlSerializer(typeof(DeputadoListaPresencaSoap));
                using (XmlNodeReader reader = new XmlNodeReader(node))
                {
                    deputadoListaPresenca = (DeputadoListaPresencaSoap)serializerDeputado.Deserialize(reader);
                }

                return deputadoListaPresenca;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }
}