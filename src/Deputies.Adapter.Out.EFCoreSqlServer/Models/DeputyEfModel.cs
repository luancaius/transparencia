// Deputies.Adapter.Out.EFCoreSqlServer/Models/DeputyEfModel.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Models;

[Table("Deputies")]
public class DeputyEfModel
{
    [Key]
    public int Id { get; set; }

    public string DeputyName { get; set; }
    public string Party { get; set; }

    public string SourcesJson { get; set; }

    // Relationship to Person
    public int PersonId { get; set; }

    [ForeignKey(nameof(PersonId))]
    public PersonEfModel Person { get; set; }

    public DeputyEfModel() { }
}