using System.Xml.Serialization;

namespace Repositories.DTO.OldApi.GetAll;

[XmlRoot(ElementName = "deputados")]
public class DeputiesListOldApi
{
    public DeputiesListOldApi(String rawDeputiesList)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(DeputiesListOldApi));
            var reader = new StringReader(rawDeputiesList);
            var deputies = (DeputiesListOldApi)serializer.Deserialize(reader);
            Deputies = deputies.Deputies;   
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<DeputyOldApi> Deputies { get; set; }
}