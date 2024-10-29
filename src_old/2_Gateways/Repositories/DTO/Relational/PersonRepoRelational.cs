namespace Repositories.DTO.Relational;

public class PersonRepoRelational
{
    public string NomeCivil { get; set; }
    public string Cpf { get; set; }
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; } 
    public string Email { get; set; }
    public string UfNascimento { get; set; }
    public string MunicipioNascimento { get; set; }
    public string Escolaridade { get; set; }

    public static PersonRepoRelational ConvertFrom(DeputyDetailRepoRelational deputyDetailRepoRelational)
    {
        return new PersonRepoRelational()
        {
            NomeCivil = deputyDetailRepoRelational.NomeCivil,
            Cpf = deputyDetailRepoRelational.Cpf,
            Sexo = deputyDetailRepoRelational.Sexo,
            DataNascimento = deputyDetailRepoRelational.DataNascimento,
            Email = deputyDetailRepoRelational.Email,
            UfNascimento = deputyDetailRepoRelational.UfNascimento,
            MunicipioNascimento = deputyDetailRepoRelational.MunicipioNascimento,
            Escolaridade = deputyDetailRepoRelational.Escolaridade
        };
    }
}