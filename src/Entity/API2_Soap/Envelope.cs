using System.Xml.Serialization;

namespace Entity.API2_Soap;

[XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", ElementName = "soap:Envelope")]
public class Envelope { 

    public Body Body { get; set; }
}