using System.ComponentModel.DataAnnotations;

namespace Entity.Geral;

public class PessoaJuridica : BaseEntityGuid
{     
    [Required]
    [MaxLength(100)]
    public string NomeFantasia { get; set; }
        
    [MaxLength(100)]
    public string NomeLegal { get; set; }
    
    [MaxLength(20)]
    public string Cnpj { get; set; }
        
    [MaxLength(200)]
    public string Endereco { get; set; }
    
    public List<PessoaFisica> Donos { get; set; }
}