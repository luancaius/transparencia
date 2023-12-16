using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RelationalDatabase.DTO.Deputado;

[Table("Deputado", Schema = "congresso")]
public class Deputado : BaseEntity
{
    [StringLength(200)]
    public string? Uri { get; set; }

    [StringLength(100)]
    public string Nome { get; set; }

    [StringLength(100)]
    public string NomeEleitoral { get; set; }

    [StringLength(100)]
    public string NomeCivil { get; set; }

    [StringLength(100)]
    public string? NomeParlamentarAtual { get; set; }

    [StringLength(20)]
    public string SiglaPartido { get; set; }

    [StringLength(200)]
    public string? UriPartido { get; set; }

    [StringLength(2)]
    public string SiglaUf { get; set; }

    public int Legislatura { get; set; }

    [StringLength(200)]
    public string? UrlFoto { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string Sexo { get; set; }

    [StringLength(2)]
    public string UfRepresentacaoAtual { get; set; }

    [StringLength(50)]
    public string SituacaoNaLegislaturaAtual { get; set; }

    [StringLength(11)]
    public string Cpf { get; set; }

    public DateTime DataNascimento { get; set; }

    public DateTime? DataFalecimento { get; set; }

    [StringLength(2)]
    public string UfNascimento { get; set; }

    [StringLength(50)]
    public string MunicipioNascimento { get; set; }

    [StringLength(100)]
    public string? Escolaridade { get; set; }

    [StringLength(200)]
    public string? UrlWebsite { get; set; }

    [StringLength(600)]
    public string? RedeSocial { get; set; }

    public DateTime? Data { get; set; }

    [StringLength(50)]
    public string Situacao { get; set; }

    [StringLength(50)]
    public string CondicaoEleitoral { get; set; }
    
    public virtual ICollection<Comissao> Comissoes { get; set; }
    public virtual ICollection<DeputyExpenses> Expenses { get; set; }
    public virtual ICollection<DeputyWorkPresence> WorkPresences { get; set; }
}
