namespace Kontroll.Database.Model.TransactionModels;

public class SupplierOb
{
    public string? UserId { get; set; }
    public string? SupplierId { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyAddress { get; set; }
    public string? CompanyPhoneNumber { get; set; }
    public List<string>? CompanyEmails { get; set; }
    public string? TypeOfGoods  { get; set; }
}