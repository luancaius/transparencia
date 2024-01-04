using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.DTO;

[Table("fornecedores", Schema = "general")]
[Index(nameof(Cnpj), IsUnique = true)] 
[Index(nameof(Cpf), IsUnique = true)] 
public class Supplier : BaseEntity
{
    [StringLength(100)] 
    public string Name { get; set; }
    [StringLength(14)] 
    public string Cnpj { get; set; }
    [StringLength(11)] 
    public string Cpf { get; set; }
}