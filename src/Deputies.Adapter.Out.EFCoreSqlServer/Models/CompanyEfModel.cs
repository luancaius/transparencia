using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Models
{
    [Table("Companies")]
    [Index(nameof(Cnpj), IsUnique = true)]
    public class CompanyEfModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(14)] // Typical length for a CNPJ (just the digits)
        public string Cnpj { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; } = string.Empty;
    }
}