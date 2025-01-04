
namespace Deputies.Adapter.Out.ExternalAPI.Dtos;

public class DeputyExpensesDto
{
    public IEnumerable<ExpenseDto> Dados { get; set; }
}
    
public class ExpenseDto
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public string TipoDespesa { get; set; }
    public int CodDocumento { get; set; }
    public string TipoDocumento { get; set; }
    public int CodTipoDocumento { get; set; }
    public DateTime DataDocumento { get; set; }
    public string NumDocumento { get; set; }
    public decimal ValorDocumento { get; set; }
    public string UrlDocumento { get; set; }
    public string NomeFornecedor { get; set; }
    public string CnpjCpfFornecedor { get; set; }
    public decimal ValorLiquido { get; set; }
    public decimal ValorGlosa { get; set; }
    public string NumRessarcimento { get; set; }
    public int CodLote { get; set; }
    public int Parcela { get; set; }
}