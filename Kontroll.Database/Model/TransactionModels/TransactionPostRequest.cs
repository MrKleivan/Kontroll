namespace Kontroll.Database.Model.TransactionModels;

public class TransactionPostRequest
{
    public string TransactionId { get; set; }
    public string UserId { get; set; }
    public DateOnly Date { get; set; }
    public string AccountNumber { get; set; }
    public string? ExternalDescription  { get; set; }
    public string? UserDescription  { get; set; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }
    public string ToAccount { get; set; }
    public string FromAccount { get; set; }
    public string? SupplierId { get; set; }
    public bool IsFixedPayment { get; set; }
    public string? FixedPaymentId { get; set; }
    public bool ForceAdd { get; set; } = false;
}