// Deputies.Adapter.Out.EFCoreSqlServer/Models/PersonEfModel.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Models;

[Table("Persons")]
public class PersonEfModel
{
    [Key]
    public int Id { get; set; }

    // We want this to be unique
    [Required] // ensures it's not null
    [MaxLength(11)] // typical length for CPF (without punctuation)
    public string Cpf { get; set; }
        
    // Break out the "Name" value object
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }

    public PersonEfModel() { }
}