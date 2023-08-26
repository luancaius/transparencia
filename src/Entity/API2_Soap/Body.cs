using System.Xml.Serialization;

namespace Entity.API2_Soap;

[XmlRoot(ElementName="soap:Body")]
public class Body { 

    public ObterDeputadosResponse ObterDeputadosResponse { get; set; } 
}