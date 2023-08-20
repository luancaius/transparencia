

using System.Xml.Serialization;

namespace Entity.API2_Soap;


[XmlRoot(ElementName="comissoes")]
public class Comissoes { 

	[XmlElement(ElementName="titular")] 
	public object Titular { get; set; } 

	[XmlElement(ElementName="suplente")] 
	public object Suplente { get; set; } 
}

[XmlRoot(ElementName="deputado")]
public class Deputado { 

	[XmlElement(ElementName="ideCadastro")] 
	public int IdeCadastro { get; set; } 

	[XmlElement(ElementName="codOrcamento")] 
	public int CodOrcamento { get; set; } 

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
}

[XmlRoot(ElementName = "deputados")]
public class Deputados
{
	[XmlElement(ElementName = "deputado")]
	public List<Deputado> Deputado { get; set; }
}

[XmlRoot(ElementName="ObterDeputadosResult")]
public class ObterDeputadosResult { 

	[XmlElement(ElementName="deputados", Namespace = "")] 
	public Deputados Deputados { get; set; } 
}

[XmlRoot(ElementName="ObterDeputadosResponse")]
public class ObterDeputadosResponse { 

	[XmlElement(ElementName="ObterDeputadosResult")] 
	public ObterDeputadosResult ObterDeputadosResult { get; set; }
}

[XmlRoot(ElementName="Body")]
public class Body { 

	[XmlElement(ElementName="ObterDeputadosResponse", Namespace = "https://www.camara.gov.br/SitCamaraWS/Deputados")] 
	public ObterDeputadosResponse ObterDeputadosResponse { get; set; } 
}

[XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class Envelope { 

	[XmlElement(ElementName="Body")] 
	public Body Body { get; set; } 
    
}

