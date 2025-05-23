namespace Kontroll.Database.Model.TransactionModels;

public class ExpectedTransactionOb
{
    public string? ExpectedTransactionId { get; set; }
    public string? FixedExpenseId { get; set; }
    public string? UserId { get; set; }
    public string? AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public DateOnly DueDate { get; set; } 
    public bool IsSettled { get; set; } = false;
    public string? MatchedTransactionId  { get; set; }
}