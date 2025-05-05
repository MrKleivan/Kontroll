namespace Kontroll.Database.Model.TransactionModels;

public class InvoiceOb
{
    public string? UserId { get; set; }
    public string? FixedExpenseId { get; set; }
    public string? SupplierId { get; set; }
    public string? Description { get; set; }
    public string? TypeOfInvoice { get; set; }
    public string? InvoiceSentOn { get; set; }
    public decimal InvoiceAmount { get; set; }
    public string? InvoiceKidNumber { get; set; } 
    public bool IsPaid { get; set; }
    public DateOnly SettledDate { get; set; }
    public string? TransactionId { get; set; }
}