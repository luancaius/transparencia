namespace Deputies.Application.Dtos;

public record DeputyExpensesDto(
    int Ano,
    int Mes,
    string TipoDespesa,
    int CodDocumento,
    string TipoDocumento,
    int CodTipoDocumento,
    DateTime DataDocumento,
    string NumDocumento,
    decimal ValorDocumento,
    string UrlDocumento,
    string NomeFornecedor,
    string CnpjCpfFornecedor,
    decimal ValorLiquido
);