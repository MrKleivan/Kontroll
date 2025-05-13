using System.ComponentModel.DataAnnotations.Schema;

namespace Kontroll.Database.Model.TransactionModels;

[Table("InvoiceTb")]
public class InvoiceOb
{
    public string? UserId { get; set; }
    public string? InvoiceId { get; set; }
    public string? FixedExpenseId { get; set; }
    public string? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierBankAccountNumber { get; set; }
    public string? Description { get; set; }
    public string? TypeOfInvoice { get; set; }
    public string? InvoiceSentOn { get; set; }
    public decimal InvoiceAmount { get; set; }
    public string? InvoiceKidNumber { get; set; }
    public DateOnly InvoiceDate { get; set; }
    public DateOnly InvoiceDeadlineDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string? PayedFromAccountNumber { get; set; }
    public bool IsFullySettled { get; set; }
    public DateOnly SettledDate { get; set; }
    public string? TransactionId { get; set; }
    public bool HasDebtCollection { get; set; }
    public string? DebtCollectionId { get; set; }
}