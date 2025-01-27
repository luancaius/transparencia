// Deputies.Adapter.Out.EFCoreSqlServer/Models/PersonEfModel.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Models;

[Table("Persons")]
[Index(nameof(Cpf), IsUnique = true)]
public class PersonEfModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(11)] 
    public string Cpf { get; set; }
        
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }

    public PersonEfModel() { }
}