namespace Deputies.Adapter.Out.ExternalAPI.Dtos;

public class DeputadoListDto
{
    public int id { get; set; }
    public string nome { get; set; }
    public string siglaPartido { get; set; }
    public string siglaUf { get; set; }
    public int idLegislatura { get; set; }
    public string email { get; set; }
}