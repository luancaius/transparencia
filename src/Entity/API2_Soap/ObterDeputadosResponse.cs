using System.Xml.Serialization;

namespace Entity.API2_Soap;

[XmlRoot(ElementName="ObterDeputadosResponse", Namespace = "https://www.camara.gov.br/SitCamaraWS/Deputados")]
public class ObterDeputadosResponse { 

    public ObterDeputadosResult ObterDeputadosResult { get; set; }
}