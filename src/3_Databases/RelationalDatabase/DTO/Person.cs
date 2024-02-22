using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.DTO;

[Table("People", Schema = "General")]
public class Person : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    [Required]
    [StringLength(11)]
    public string Cpf { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(20)]
    public string EstadoNascimento { get; set; }

    [StringLength(30)]
    public string MunicipioNascimento { get; set; }

    [StringLength(20)]
    public string Gender { get; set; }

    [StringLength(50)]
    public string Escolaridade { get; set; }

}