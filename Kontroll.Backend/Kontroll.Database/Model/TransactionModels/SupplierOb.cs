namespace Kontroll.Database.Model.TransactionModels;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SupplierTb")]
public class SupplierOb
{
    public string? UserId { get; set; }
    public string? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierAddress { get; set; }
    public string? SupplierPhoneNumber { get; set; }
    public string? SupplierEmails { get; set; }
    public string? TypeOfGoods  { get; set; }
    public string? WebSite  { get; set; }
}