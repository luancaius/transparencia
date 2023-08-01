using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class PessoaFisica
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
        
    public int? Idade { get; set; }
        
    [MaxLength(200)]
    public string Endereco { get; set; }
}