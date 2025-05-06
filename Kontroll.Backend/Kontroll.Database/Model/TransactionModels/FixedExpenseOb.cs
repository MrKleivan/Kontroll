namespace Kontroll.Database.Model.TransactionModels;

public class FixedExpenseOb
{
    public string? UserId { get; set; }
    public string? FixedExpenseId  { get; set; }
    public string? SupplierId { get; set; }
    public string? SupplierBankAccount { get; set; }
    public string? Description { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal MonthlyAmount { get; set; }
    public int MonthlyDeadlineDay { get; set; }
    public bool HasPayments { get; set; }
    public bool IsFullyPaid { get; set; } = false;
    public DateOnly? SettledDate { get; set; } 
    public string? StandardAccountNumberForePayment { get; set; }
}