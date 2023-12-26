using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.DTO.Deputado;

[Table("deputado", Schema = "congresso")]
[Index(nameof(Cpf), IsUnique = true)] 
public class Deputado : BaseEntity
{
    [StringLength(50)]
    public string NomeEleitoral { get; set; }

    [StringLength(100)]
    public string NomeCivil { get; set; }

    [StringLength(20)]
    public string SiglaPartido { get; set; }

    [StringLength(11)]
    public string Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    [StringLength(2)]
    public string UfNascimento { get; set; }

    [StringLength(10)]
    public string Sexo { get; set; }

    [StringLength(15)]
    public string UfRepresentacaoAtual { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }
    
    [StringLength(10)]
    public string IdApi { get; set; }
}