using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class PessoaJuridica
{
    public int Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
        
    [MaxLength(20)]
    public string Cnpj { get; set; }
        
    [MaxLength(200)]
    public string Endereco { get; set; }
}