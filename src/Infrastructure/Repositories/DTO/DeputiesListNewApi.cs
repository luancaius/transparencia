namespace Repositories.DTO;

public class DeputiesListNewApi
{
    public string Name { get; set; }
    public DeputiesListNewApi(String rawDeputiesList)
    {
        Name = "";
    }
}