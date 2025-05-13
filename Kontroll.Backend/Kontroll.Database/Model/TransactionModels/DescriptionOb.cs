using System.ComponentModel.DataAnnotations.Schema;

namespace Kontroll.Database.Model.TransactionModels;

[Table("DescriptionTb")]
public class DescriptionOb
{
    public string UserId { get; set; }
    public int Id { get; set; }
    public string? ExternalDescription { get; set; }
    public string? UserDescription { get; set; }
}