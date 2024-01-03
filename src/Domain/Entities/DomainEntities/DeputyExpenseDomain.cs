namespace Entities.DomainEntities;

public class DeputyExpenseDomain
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
    public SupplierDomain Supplier { get; private set; }

    
    private DeputyExpenseDomain(DateTime dateTimeExpense, int deputyId, decimal amountDocument, decimal amountFinal, 
        string receiptUrl, string typeExpense, string typeReceipt, string numberDocument, string idDocument, 
        SupplierDomain supplierDomain)
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
        Supplier = supplierDomain;
    }
    
    public static DeputyExpenseDomain CreateExpense(DateTime dateTimeExpense, int deputyId, decimal amountDocument, 
        decimal amountFinal, string receiptUrl, string typeExpense, string typeReceipt, string numberDocument, 
        string idDocument, string  cnpjCompany, string nameCompany)
    {
        var supplier = SupplierDomain.CreateSupplier(nameCompany, cnpjCompany);
        return new DeputyExpenseDomain(dateTimeExpense, deputyId, amountDocument, amountFinal, receiptUrl, typeExpense, 
            typeReceipt, numberDocument, idDocument, supplier);
    }
}