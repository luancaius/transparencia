using System.Xml.Serialization;

namespace Entity.API2_Soap;

[XmlRoot(ElementName="ObterDeputadosResult")]
public class ObterDeputadosResult { 

    public DeputadosSoap Deputados { get; set; } 
}