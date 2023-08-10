using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Geral;

namespace Entity.Congresso;

public class Deputado : BaseEntity
{        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    public Partido? Partido { get; set; }
    public Legislatura Legislatura { get; set; }

    public PessoaFisica PessoaFisica { get; set; }
    
    public override string ToString()
    {
        return $"Nome: {Nome}, Partido: {Partido?.Nome}, Legislatura: {Legislatura?.Numero}";
    }
}