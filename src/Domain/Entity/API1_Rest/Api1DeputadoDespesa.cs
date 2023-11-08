namespace Entity.API1_Rest;

public class Api1DeputadoDespesa
{
    public int Ano { get; set; }
    public string CnpjCpfFornecedor { get; set; }
    public int CodDocumento { get; set; }
    public int CodLote { get; set; }
    public int CodTipoDocumento { get; set; }
    public string DataDocumento { get; set; }
    public int Mes { get; set; }
    public string NomeFornecedor { get; set; }
    public string NumDocumento { get; set; }
    public string NumRessarcimento { get; set; }
    public int Parcela { get; set; }
    public string TipoDespesa { get; set; }
    public string TipoDocumento { get; set; }
    public string UrlDocumento { get; set; }
    public decimal ValorDocumento { get; }
    public decimal ValorGlosa { get; set; }
    public decimal ValorLiquido { get; set; }
}