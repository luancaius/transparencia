using System.Xml.Serialization;

namespace Entity.API2_Soap.GetListaPresenca;

[XmlRoot("parlamentar")]
public class DeputadoListaPresencaSoap
{
    [XmlElement("legislatura")]
    public int Legislatura { get; set; }

    [XmlElement("carteiraParlamentar")]
    public int CarteiraParlamentar { get; set; }

    [XmlElement("nomeParlamentar")]
    public string NomeParlamentar { get; set; }

    [XmlElement("siglaPartido")]
    public string SiglaPartido { get; set; }

    [XmlElement("siglaUF")]
    public string SiglaUF { get; set; }

    [XmlArray("diasDeSessoes2")]
    [XmlArrayItem("dia")]
    public List<Dia> DiasDeSessoes { get; set; }
}