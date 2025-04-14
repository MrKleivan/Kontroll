namespace Kontroll.Database.Model.TransactionModels;

public class TransactionOb
{
    public string TransactionId { get; set; }
    public string UserId { get; set; }
    public DateOnly Date { get; set; }
    public string AccountNumber { get; set; }
    public string Description  { get; set; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }
    public string ToAccount { get; set; }
    public string FromAccount { get; set; }
    public bool IsFixedPayment { get; set; } = false;
    public string? FixedPaymentId { get; set; }
}