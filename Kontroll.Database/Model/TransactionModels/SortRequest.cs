namespace Kontroll.Database.Model.TransactionModels;

public class SortRequest
{
    public string UserId { get; set; }
    public string TransactionId { get; set; }
    public string Request { get; set; }
    public string Description { get; set; }
    public string AccountNumber { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public string AmountDirection { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}