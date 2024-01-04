using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("DeputadoDespesa", Schema = "congresso")]
public class DeputyExpense : BaseEntity
{
    [Column(TypeName = "datetime2")] 
    public DateTime? DateTimeExpense { get; set; } // Stored as DateTime

    [Column(TypeName = "decimal(18, 2)")] 
    public decimal AmountDocument { get; set; }

    [Column(TypeName = "decimal(18, 2)")] 
    public decimal AmountFinal { get; set; }

    [StringLength(200)] 
    public string ReceiptUrl { get; set; }

    [StringLength(100)] 
    public string TypeExpense { get; set; }

    [StringLength(100)] 
    public string TypeReceipt { get; set; }

    [StringLength(50)] 
    public string NumberDocument { get; set; }

    [StringLength(50)] 
    public string IdDocument { get; set; }

    public Guid SupplierId { get; set; }
    
    public int DeputyId { get; set; }
    
    [ForeignKey("SupplierId")]
    public virtual Supplier Supplier { get; set; }
}