using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.DTO;

[Table("pessoas", Schema = "general")]
[Index(nameof(Cpf), IsUnique = true)] 
public class Person : BaseEntity
{
    [StringLength(11)] 
    public string Cpf { get; set; }

    [StringLength(100)] 
    public string Name { get; set; }
}