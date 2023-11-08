namespace Repositories.DTO.OldApi.GetById;

public class Comissao
{
    public int IdOrgaoLegislativoCD { get; set; }
    public string SiglaComissao { get; set; }
    public string NomeComissao { get; set; }
    public string CondicaoMembro { get; set; }
    public string DataEntrada { get; set; } // Consider parsing to DateTime
    public string DataSaida { get; set; } // Consider parsing to DateTime
}