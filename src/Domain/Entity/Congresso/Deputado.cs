using System.ComponentModel.DataAnnotations;
using Entity.Geral;

namespace Entity.Congresso;

public class Deputado : BaseEntity
{        
    [Required]
    [MaxLength(100)]
    public string Nome { get; }
    public Partido? Partido { get; }
    public Legislatura Legislatura { get; }

    public PessoaFisica PessoaFisica { get; set; }
    
    public override string ToString()
    {
        return $"Nome: {Nome}, Partido: {Partido?.Nome}, Legislatura: {Legislatura?.Numero}";
    }
}