namespace Kontroll.Database.Model.TransactionModels;

public class DescriptionOb
{
    public string UserId { get; set; }
    public int Id { get; set; }
    public string? ExternalDescription { get; set; }
    public string? UserDescription { get; set; }
}