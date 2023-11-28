using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO;

[Table("Gabinete", Schema = "Congresso")]
public class Gabinete
{
    [Key]
    public int GabineteId { get; set; } // Assuming there is a primary key field like GabineteId
    public string Numero { get; set; }
    public string Anexo { get; set; }
    public string Telefone { get; set; }

    // Navigation properties, if there are relationships with other tables
}