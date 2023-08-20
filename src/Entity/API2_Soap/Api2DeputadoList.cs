using System.Xml.Serialization;

namespace Entity.API2_Soap
{
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        [XmlElement(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
    }

    public class Body
    {
        [XmlElement(Namespace = "https://www.camara.gov.br/SitCamaraWS/Deputados")]
        public ObterDeputadosResponse ObterDeputadosResponse { get; set; }
    }

    public class ObterDeputadosResponse
    {
        [XmlElement(Namespace = "")]
        public ObterDeputadosResult ObterDeputadosResult { get; set; }
    }

    public class ObterDeputadosResult
    {
        [XmlArray]
        [XmlArrayItem("deputado")]
        public List<Deputado> Deputados { get; set; }
    }

    public class Deputado
    {
        public string ideCadastro { get; set; }
        public string codOrcamento { get; set; }
        public string condicao { get; set; }
        public string matricula { get; set; }
        public string idParlamentar { get; set; }
        public string nome { get; set; }
        public string nomeParlamentar { get; set; }
        public string urlFoto { get; set; }
        public string sexo { get; set; }
        public string uf { get; set; }
        public string partido { get; set; }
        public string gabinete { get; set; }
        public string anexo { get; set; }
        public string fone { get; set; }
        public string email { get; set; }
        public Comissoes comissoes { get; set; }
    }

    public class Comissoes
    {
        public string titular { get; set; }
        public string suplente { get; set; }
    }
}