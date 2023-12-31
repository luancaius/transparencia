using Repositories.DTO.NewApi.Expense;

namespace Services.DTO.Deputy;

public class DeputyExpenseDto
{
    public string DateTimeExpenseStr { get; set; }
    public int DeputyId { get; set; }
    public decimal AmountDocument { get; set; }
    public decimal AmountFinal { get; set; }
    public string ReceiptUrl { get; set; }
    public string TypeExpense { get; set; }
    public string TypeReceipt { get; set; }
    public string NumberDocument { get; set; }
    public string IdDocument { get; set; }
    public string CnpjOrCpf { get; set; }
    public string NameCompany { get; set; }
    
    public static DeputyExpenseDto GetDtoFromMongo(DeputyExpense expense)
    {
        if (expense == null)
        {
            throw new ArgumentNullException(nameof(expense));
        }

        return new DeputyExpenseDto
        {
            DateTimeExpenseStr = expense.DataDocumento.HasValue ? expense.DataDocumento.Value.ToString("yyyy-MM-dd") : null,
            DeputyId = expense.IdDeputy,
            AmountDocument = Convert.ToDecimal(expense.ValorDocumento),
            AmountFinal = Convert.ToDecimal(expense.ValorLiquido),
            ReceiptUrl = expense.UrlDocumento,
            TypeExpense = expense.TipoDespesa,
            TypeReceipt = expense.TipoDocumento,
            NumberDocument = expense.NumDocumento,
            IdDocument = expense.CodDocumento.ToString(),
            CnpjOrCpf = expense.CnpjCpfFornecedor,
            NameCompany = expense.NomeFornecedor
        };
    }
}