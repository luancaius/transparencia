using System.Xml.Serialization;

namespace Repositories.DTO.OldApi.GetAll;

public class DeputiesListOldApi
{
    public DeputiesListOldApi(String rawDeputiesList)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(DeputiesListOldApiSerializer));
            var reader = new StringReader(rawDeputiesList);
            var deputies = (DeputiesListOldApiSerializer)serializer.Deserialize(reader);
            Console.WriteLine(deputies);
            
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        // try
        // {
        //     XmlDocument doc = new XmlDocument();
        //     doc.LoadXml(rawDeputiesList);
        //     XmlNodeList nodes = doc.DocumentElement.SelectNodes("//deputados/deputado");
        //     var serializer = new XmlSerializer(typeof(DeputiesListOldApiSerializer));
        //     foreach (XmlNode node in nodes)
        //     {
        //         using (XmlNodeReader reader = new XmlNodeReader(node))
        //         {
        //             var deputado = (DeputyOldApi)serializer.Deserialize(reader);
        //             Console.WriteLine(deputado);
        //         }
        //     }
        // }
    }

}

[XmlRoot(ElementName = "deputados")]
public class DeputiesListOldApiSerializer
{
    public List<DeputyOldApi> Deputies { get; set; }
}