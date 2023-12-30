using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationalDatabase.DTO.Deputado;

[Table("DeputadoDespesa", Schema = "congresso")]
public class DeputyExpense
{
    [Key] 
    public int DeputyExpenseId { get; set; } // Assuming there is an ID field

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

    public int CompanyId { get; set; }
    
    public int DeputyId { get; set; }
    
    [ForeignKey("CompanyId")]
    public virtual Company Company { get; set; }
}