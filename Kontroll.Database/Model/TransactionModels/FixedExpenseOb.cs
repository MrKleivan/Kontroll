namespace Kontroll.Database.Model.TransactionModels;

public class FixedExpenseOb
{
    public string FixedExpenseId  { get; set; }
    public string UserId { get; set; }
    public string Claimant { get; set; }
    public string Description { get; set; }
    public decimal MonthlyAmount { get; set; }
    public DateOnly MonthlyDeadlineDate { get; set; }
    public bool IsSettled { get; set; } = false;
    public DateOnly SettledDate { get; set; }
    public decimal AmountPayed { get; set; }
    public string PaymentMethod { get; set; }
    public string StandarAccountNumberForePayment { get; set; }
    public string AccountNumberUsedForePayment { get; set; }
    public string TransactionId { get; set; }
}