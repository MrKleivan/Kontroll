using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.FixedExpenseTests;

public class FixedExpenseTestOne
{
    public async Task Run(IConfiguration config)
    {
        FixedExpenseController app = new FixedExpenseController(config);
        
        FixedExpenseOb fixedExpenseOb = new FixedExpenseOb
        {
            FixedExpenseId = Guid.NewGuid().ToString(),
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            SupplierId = null,
            SupplierBankAccount = "81013009090",
            Description = "Forsikkring",
            PaymentMethod = null,
            MonthlyAmount = -3481,
            MonthlyDeadlineDay = 20,
            FixedExpenseStartDate = new DateOnly(),
            IsActive = true,
            StandardAccountNumberForePayment = "Regningskonto"
        };
        
        await app.AddFixedExpense(fixedExpenseOb);

        List<FixedExpenseOb> des = await app.GetFixedExpensesByUserId(fixedExpenseOb);

        foreach (var d in des)
        {
            Console.WriteLine(d.Description + " " + d.SupplierId + " " + d.SupplierBankAccount + " " + d.PaymentMethod);
        }
    }
}