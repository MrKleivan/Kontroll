namespace Kontroll.Database.Model.TransactionModels;

public class SortRequest
{
    public string UserId { get; set; }
    public string TransactionId { get; set; }
    public string Request { get; set; }
    public string ExternalDescription { get; set; }
    public string UserDescription { get; set; }
    public string? UserAccountNumber { get; set; }
    public string? SupplierAccountNumber { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public string AmountDirection { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}