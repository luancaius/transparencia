using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RelationalDatabase.DTO.Deputado;

[Table("deputados", Schema = "congresso")]
[Index(nameof(IdApi), IsUnique = true)] 
public class Deputado : BaseEntity
{
    [StringLength(50)]
    public string NomeEleitoral { get; set; }
    [StringLength(20)]
    public string SiglaPartido { get; set; }
    [StringLength(20)]
    public string UfNascimento { get; set; }
    [StringLength(15)]
    public string UfRepresentacaoAtual { get; set; }
    [StringLength(50)]
    public string? Email { get; set; }
    [StringLength(10)]
    public string IdApi { get; set; }
    
    public virtual Person Person { get; set; }
    public virtual ICollection<DeputyExpense> DeputyExpenses { get; set; }
}