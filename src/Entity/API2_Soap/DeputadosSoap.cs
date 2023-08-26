using System.Xml.Serialization;

namespace Entity.API2_Soap;

[XmlRoot(ElementName = "deputados")]
public class DeputadosSoap
{
    public List<DeputadoSoap> Deputado { get; set; }
}