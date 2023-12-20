using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("Deputado", Schema = "congresso")]
public class Deputado : BaseEntity
{
    [StringLength(100)]
    public string Nome { get; set; }

    [StringLength(100)]
    public string NomeEleitoral { get; set; }

    [StringLength(100)]
    public string NomeCivil { get; set; }

    [StringLength(20)]
    public string SiglaPartido { get; set; }

    [StringLength(2)]
    public string SiglaUf { get; set; }

    [StringLength(11)]
    public string Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    [StringLength(2)]
    public string UfNascimento { get; set; }

    [StringLength(10)]
    public string Sexo { get; set; }

    [StringLength(2)]
    public string UfRepresentacaoAtual { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }
}