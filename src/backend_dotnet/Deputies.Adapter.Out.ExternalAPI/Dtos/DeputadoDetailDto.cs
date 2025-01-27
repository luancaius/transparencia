namespace Deputies.Adapter.Out.ExternalAPI.Dtos;

public class DeputadoDetailDto
{
    public int id { get; set; }
    public string nomeCivil { get; set; }
    public string cpf { get; set; }
    public string sexo { get; set; }
    public DateTime dataNascimento { get; set; }
    public string ufNascimento { get; set; }
    public string municipioNascimento { get; set; }
    public string escolaridade { get; set; }
    public UltimoStatusDto ultimoStatus { get; set; }
}

public class UltimoStatusDto
{
    public string nome { get; set; }
    public string siglaPartido { get; set; }
    public string siglaUf { get; set; }
    public int idLegislatura { get; set; }
    public string email { get; set; }
}