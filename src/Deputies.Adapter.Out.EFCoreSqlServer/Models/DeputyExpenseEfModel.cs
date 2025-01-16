using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deputies.Adapter.Out.EFCoreSqlServer.Models;

[Table("DeputyExpenses")]
public class DeputyExpenseEfModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime Date { get; set; }
        
    [MaxLength(2048)]
    public string? UrlDocument { get; set; }

    public int? DeputyId { get; set; }
        
    [ForeignKey(nameof(DeputyId))]
    public DeputyEfModel? Deputy { get; set; }

    public int BuyerPersonId { get; set; }

    [ForeignKey(nameof(BuyerPersonId))]
    public PersonEfModel Buyer { get; set; } = default!;

    [Required]
    [MaxLength(14)]
    public string SupplierCpfCnpj { get; set; } = string.Empty;
}