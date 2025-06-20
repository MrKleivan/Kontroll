namespace Kontroll.Database.Model.TransactionModels;

public class BankReconciliationOb
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? AccountNumber { get; set; }
    public int PeriodMonth { get; set; }
    public int PeriodYear { get; set; }
    public DateOnly BankReconciliationSetDate { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal PeriodResult { get; set; }
    public decimal PeriodTurnover  { get; set; }
    public string? BankaccountId  { get; set; }
}