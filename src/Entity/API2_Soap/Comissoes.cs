

using System.Xml.Serialization;

namespace Entity.API2_Soap;

[XmlRoot(ElementName="comissoes")]
public class Comissoes { 

	[XmlElement(ElementName="titular")] 
	public object? Titular { get; set; } 

	[XmlElement(ElementName="suplente")] 
	public object? Suplente { get; set; } 
}