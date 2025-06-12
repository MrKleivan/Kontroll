namespace Kontroll.Database.Model.TransactionModels;

public class FixedTransactionsOb
{
    public string? UserId { get; set; }
    public string? FixedTransactionId  { get; set; }
    public string? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierBankAccount { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal MonthlyAmount { get; set; }
    public int MonthlyDeadlineDay { get; set; }
    public DateOnly FixedTransactionStartDate { get; set; }
    public bool IsActive { get; set; }
    public DateOnly? FixedTransactionEndDate { get; set; } = null;
    public string? StandardUserAccountNumberForeTransaction { get; set; }
}