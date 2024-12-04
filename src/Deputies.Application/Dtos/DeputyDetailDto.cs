namespace Deputies.Application.Dtos;

public record DeputyDetailDto(
    int Id,
    string NomeCivil,
    string Cpf,
    string Sexo,
    DateTime DataNascimento,
    string UfNascimento,
    string MunicipioNascimento,
    string Escolaridade,
    string Nome,
    string SiglaPartido,
    string SiglaUf,
    int IdLegislatura,
    string Email
    );