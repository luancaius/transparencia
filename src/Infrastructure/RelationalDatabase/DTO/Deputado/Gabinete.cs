using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO;

[Table("Gabinete", Schema = "congresso")]
public class Gabinete : BaseEntity
{
    public string Numero { get; set; }
    public string Anexo { get; set; }
    public string Telefone { get; set; }
}