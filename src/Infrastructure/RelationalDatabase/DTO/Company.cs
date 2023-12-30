using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO;

[Table("empresa", Schema = "general")]
public class Company
{
    [Key] 
    public int CompanyId { get; set; }

    [StringLength(14)] 
    public string Cnpj { get; set; }

    [StringLength(100)] 
    public string Name { get; set; }
}