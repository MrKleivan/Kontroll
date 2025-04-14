namespace Kontroll.Database.Model.TransactionModels;

public class FixedExpenses
{
    public Guid FixedExpenseId  { get; set; }
    public Guid UserId { get; set; }
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
}