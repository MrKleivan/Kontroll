using System.ComponentModel.DataAnnotations.Schema;

namespace Kontroll.Database.Model.TransactionModels;

[Table("TransactionTb")]
public class TransactionOb
{
    public string? UserId { get; set; }
    public string? TransactionId { get; set; }
    public DateOnly Date { get; set; }
    public string? AccountNumber { get; set; }
    public string? ExternalDescription  { get; set; }
    public string? UserDescription  { get; set; }
    public decimal Income { get; set; }
    public decimal Outcome { get; set; }
    public string? ToAccount { get; set; }
    public string? FromAccount { get; set; }
    public string? SupplierId { get; set; }
    public bool IsFixedExpense { get; set; } = false;
    public string? FixedTransactionId { get; set; }
    public bool HasReceipt {get; set; } = false;
    public string? ReceiptId { get; set; }
    public bool HasInvoice { get; set; } = false;
    public string? InvoiceId { get; set; }
}