using System.Text.Json.Serialization;

namespace Deputies.Adapter.Out.ExternalAPI.Dtos;

public record ExpenseDto(
    [property: JsonPropertyName("ano")] int Ano,
    [property: JsonPropertyName("mes")] int Mes,
    [property: JsonPropertyName("tipoDespesa")] string TipoDespesa,
    [property: JsonPropertyName("codDocumento")] int CodDocumento,
    [property: JsonPropertyName("tipoDocumento")] string TipoDocumento,
    [property: JsonPropertyName("codTipoDocumento")] int CodTipoDocumento,
    [property: JsonPropertyName("dataDocumento")] DateTime DataDocumento,
    [property: JsonPropertyName("numDocumento")] string NumDocumento,
    [property: JsonPropertyName("valorDocumento")] decimal ValorDocumento,
    [property: JsonPropertyName("urlDocumento")] string? UrlDocumento, // Nullable for potential null values
    [property: JsonPropertyName("nomeFornecedor")] string NomeFornecedor,
    [property: JsonPropertyName("cnpjCpfFornecedor")] string? CnpjCpfFornecedor, // Nullable for potential null or empty strings
    [property: JsonPropertyName("valorLiquido")] decimal ValorLiquido,
    [property: JsonPropertyName("valorGlosa")] decimal ValorGlosa,
    [property: JsonPropertyName("numRessarcimento")] string? NumRessarcimento, // Nullable for potential null or empty strings
    [property: JsonPropertyName("codLote")] int CodLote,
    [property: JsonPropertyName("parcela")] int Parcela
);