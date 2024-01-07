using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.DTO;

[Table("empresas", Schema = "general")]
[Index(nameof(Cnpj), IsUnique = true)] 
public class Company : BaseEntity
{
    [StringLength(14)] 
    public string Cnpj { get; set; }

    [StringLength(100)] 
    public string Name { get; set; }
}