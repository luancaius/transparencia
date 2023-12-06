using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("Comissao", Schema = "congresso")]
public class Comissao
{
    [Key]
    public int IdOrgaoLegislativoCD { get; set; }
    public string SiglaComissao { get; set; }
    public string NomeComissao { get; set; }
    public string CondicaoMembro { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataEntrada { get; set; } // Changed to DateTime

    [DataType(DataType.Date)]
    public DateTime? DataSaida { get; set; } // Changed to DateTime

    // Navigation properties, if any, should be included here
}