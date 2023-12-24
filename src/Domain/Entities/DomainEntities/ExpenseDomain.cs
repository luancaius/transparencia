namespace Entities.DomainEntities;

public class ExpenseDomain
{
    public DateTime DateTimeExpense { get; private set; }
    public int DeputyId { get; private set; }
    public decimal AmountDocument { get; private set; }
    public decimal AmountFinal { get; private set; }
    public string ReceiptUrl { get; private set; }
    public string TypeExpense { get; private set; }
    public string TypeReceipt { get; private set; }
    public string NumberDocument { get; private set; }
    public string IdDocument { get; private set; }
    public CompanyDomain Company { get; private set; }
    
    private ExpenseDomain(DateTime dateTimeExpense, int deputyId, decimal amountDocument, decimal amountFinal, 
        string receiptUrl, string typeExpense, string typeReceipt, string numberDocument, string idDocument, 
        CompanyDomain company)
    {
        DateTimeExpense = dateTimeExpense;
        DeputyId = deputyId;
        AmountDocument = amountDocument;
        AmountFinal = amountFinal;
        ReceiptUrl = receiptUrl;
        TypeExpense = typeExpense;
        TypeReceipt = typeReceipt;
        NumberDocument = numberDocument;
        IdDocument = idDocument;
        Company = company;
    }
    
    public static ExpenseDomain CreateExpense(DateTime dateTimeExpense, int deputyId, decimal amountDocument, 
        decimal amountFinal, string receiptUrl, string typeExpense, string typeReceipt, string numberDocument, 
        string idDocument, string  cnpjCompany, string nameCompany)
    {
        var company = CompanyDomain.CreateCompany(nameCompany, cnpjCompany);
        return new ExpenseDomain(dateTimeExpense, deputyId, amountDocument, amountFinal, receiptUrl, typeExpense, 
            typeReceipt, numberDocument, idDocument, company);
    }
}