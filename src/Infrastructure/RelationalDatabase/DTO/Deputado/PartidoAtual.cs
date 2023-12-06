using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("PartidoAtual", Schema = "congresso")]
public class PartidoAtual : BaseEntity
{
    public string Sigla { get; set; }
    public string Nome { get; set; }

    // You might want to include navigation properties if there are any relations
}