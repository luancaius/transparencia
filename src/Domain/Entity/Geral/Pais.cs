using System.ComponentModel.DataAnnotations;

namespace Entity.Geral
{
    public class Pais : BaseEntity
    {        
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
    }
}