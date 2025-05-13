using System.ComponentModel.DataAnnotations.Schema;

namespace Kontroll.Database.Model.TransactionModels;

[Table("FixedExpenseTb")]
public class FixedExpenseOb
{
    public string? UserId { get; set; }
    public string? FixedExpenseId  { get; set; }
    public string? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierBankAccount { get; set; }
    public string? Description { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal MonthlyAmount { get; set; }
    public int MonthlyDeadlineDay { get; set; }
    public DateOnly FixedExpenseStartDate { get; set; }
    public bool IsActive { get; set; }
    public DateOnly? FixedExpenseEndDate => IsActive == true ? null : new DateOnly();
    public string? StandardAccountNumberForePayment { get; set; }
}