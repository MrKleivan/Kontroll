using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.SupplierTests;

public class SupplierTestOne
{
    public async Task Run(IConfiguration config)
    {
        SupplierController app = new SupplierController(config);
        
        SupplierOb supplierOb = new SupplierOb
        {
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            SupplierId = Guid.NewGuid().ToString(),
            SupplierName = "Storebrand Forsikring",
            SupplierAddress = null,
            SupplierPhoneNumber = null,
            SupplierEmails = null,
            TypeOfGoods = "Forsikring"
        };
        
        await app.AddSupplier(supplierOb);

        List<SupplierOb> des = await app.GetAllSuppliersByUserId(supplierOb);

        foreach (var d in des)
        {
            Console.WriteLine(d.SupplierId + " " + d.SupplierName + " " + d.TypeOfGoods + " " + d.UserId);
        }
    }
}