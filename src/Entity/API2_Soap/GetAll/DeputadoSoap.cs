using System.Xml.Serialization;

namespace Entity.API2_Soap.GetAll;

[XmlRoot(ElementName="deputado")]
public class DeputadoSoap { 

    [XmlElement(ElementName="ideCadastro")] 
    public int IdeCadastro { get; set; } 

    [XmlElement(ElementName="codOrcamento", IsNullable = true)] 
    public string CodOrcamento { get; set; } 

    [XmlElement(ElementName="condicao")] 
    public string Condicao { get; set; } 

    [XmlElement(ElementName="matricula")] 
    public int Matricula { get; set; } 

    [XmlElement(ElementName="idParlamentar")] 
    public int IdParlamentar { get; set; } 

    [XmlElement(ElementName="nome")] 
    public string Nome { get; set; } 

    [XmlElement(ElementName="nomeParlamentar")] 
    public string NomeParlamentar { get; set; } 

    [XmlElement(ElementName="urlFoto")] 
    public string UrlFoto { get; set; } 

    [XmlElement(ElementName="sexo")] 
    public string Sexo { get; set; } 

    [XmlElement(ElementName="uf")] 
    public string Uf { get; set; } 

    [XmlElement(ElementName="partido")] 
    public string Partido { get; set; } 

    [XmlElement(ElementName="gabinete")] 
    public int Gabinete { get; set; } 

    [XmlElement(ElementName="anexo")] 
    public int Anexo { get; set; } 

    [XmlElement(ElementName="fone")] 
    public string Fone { get; set; } 

    [XmlElement(ElementName="email")] 
    public string Email { get; set; } 

    [XmlElement(ElementName="comissoes")] 
    public Comissoes Comissoes { get; set; }

    public override string ToString()
    {
        return $"Nome: {Nome} idCadastro:{IdeCadastro} idParlamentar:{IdParlamentar}";
    }
}