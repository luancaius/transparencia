namespace Deputies.Adapter.Out.ExternalAPI.Dtos
{
    public record DeputyExpensesDto(IEnumerable<ExpenseDto> Dados);

    public record ExpenseDto(
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
        decimal ValorLiquido,
        decimal ValorGlosa,
        string NumRessarcimento,
        int CodLote,
        int Parcela
    );
}