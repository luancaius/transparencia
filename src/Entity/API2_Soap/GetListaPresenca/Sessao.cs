using System.Xml.Serialization;

namespace Entity.API2_Soap.GetListaPresenca;

public class Sessao
{
    [XmlElement("descricao")]
    public string Descricao { get; set; }

    [XmlElement("frequencia")]
    public string Frequencia { get; set; }
}