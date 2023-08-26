using System.Xml.Serialization;

namespace Entity.API2_Soap.GetById;


[XmlRoot(ElementName = "Deputado", Namespace = "")]
public class DeputadoByIdSoap
{
    public int numLegislatura { get; set; }
    public string email { get; set; }
    public string nomeProfissao { get; set; }
    public string dataNascimento { get; set; }
    public string dataFalecimento { get; set; }
    public string ufRepresentacaoAtual { get; set; }
    public string situacaoNaLegislaturaAtual { get; set; }
    public int ideCadastro { get; set; }
    public int idParlamentarDeprecated { get; set; }
    public string nomeParlamentarAtual { get; set; }
    public string nomeCivil { get; set; }
    public string sexo { get; set; }
    
    public PartidoAtual partidoAtual { get; set; }
    public Gabinete gabinete { get; set; }
    
    [XmlArray("comissoes")]
    [XmlArrayItem("comissao")]
    public List<Comissao> comissoes { get; set; }
    
    [XmlArray("cargosComissoes")]
    [XmlArrayItem("cargoComissoes")]
    public List<CargoComissoes> cargosComissoes { get; set; }
    
    // Add other properties as needed
}

public class PartidoAtual
{
    public string idPartido { get; set; }
    public string sigla { get; set; }
    public string nome { get; set; }
}

public class Gabinete
{
    public string numero { get; set; }
    public string anexo { get; set; }
    public string telefone { get; set; }
}

public class Comissao
{
    public int idOrgaoLegislativoCD { get; set; }
    public string siglaComissao { get; set; }
    public string nomeComissao { get; set; }
    public string condicaoMembro { get; set; }
    public string dataEntrada { get; set; }
    public string dataSaida { get; set; }
}

public class CargoComissoes
{
    public int idOrgaoLegislativoCD { get; set; }
    public string siglaComissao { get; set; }
    public string nomeComissao { get; set; }
    public int idCargo { get; set; }
    public string nomeCargo { get; set; }
    public string dataEntrada { get; set; }
    public string dataSaida { get; set; }
}