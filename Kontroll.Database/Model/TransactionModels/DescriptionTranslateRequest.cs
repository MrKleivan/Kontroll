namespace Kontroll.Database.Model.TransactionModels;

public class DescriptionTranslateRequest
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string NewDescription { get; set; }
    public bool ForceChange { get; set; } = false;
}