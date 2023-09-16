using System.Xml.Serialization;

namespace Entity.API2_Soap.GetListaPresenca;

public class Dia
{
    [XmlElement("data")]
    public string Data { get; set; }

    [XmlElement("frequencianoDia")]
    public string FrequenciaNoDia { get; set; }

    [XmlElement("justificativa")]
    public string Justificativa { get; set; }

    [XmlElement("qtdeSessoes")]
    public int QtdeSessoes { get; set; }

    [XmlArray("sessoes")]
    [XmlArrayItem("sessao")]
    public List<Sessao> Sessoes { get; set; }
}