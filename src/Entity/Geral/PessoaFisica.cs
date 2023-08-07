using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class PessoaFisica
{
    public Guid Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    
    public DateTime DataNascimento { get; set; }
        
    [MaxLength(200)]
    public string Endereco { get; set; }
}