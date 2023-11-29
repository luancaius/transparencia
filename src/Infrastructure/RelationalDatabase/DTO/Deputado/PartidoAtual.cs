using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO;

[Table("PartidoAtual", Schema = "Congresso")]
public class PartidoAtual : BaseEntity
{
    public string Sigla { get; set; }
    public string Nome { get; set; }

    // You might want to include navigation properties if there are any relations
}