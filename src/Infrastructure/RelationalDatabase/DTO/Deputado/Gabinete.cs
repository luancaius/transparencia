using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("Gabinete", Schema = "congresso")]
public class Gabinete : BaseEntity
{
    public string Telefone { get; set; }
    public string Nome { get; set; }
    public string Predio { get; set; }
    public string Sala { get; set; }
    public string Andar { get; set; }
    public string Email { get; set; }
}