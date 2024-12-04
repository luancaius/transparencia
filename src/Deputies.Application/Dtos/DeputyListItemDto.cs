namespace Deputies.Application.Dtos;

public record DeputyListItemDto(
    int Id,
    string Nome,
    string SiglaPartido,
    string SiglaUf,
    int IdLegislatura,
    string Email
);