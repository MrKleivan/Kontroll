using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.FixedExpenseTests;

public class FixedExpenseTestOne
{
    public async Task Run(IConfiguration config)
    {
        FixedTransactionController app = new FixedTransactionController(config);
        
        FixedExpenseOb fixedExpenseOb = new FixedExpenseOb
        {
            FixedExpenseId = "7896f9c6-493e-441e-893c-f89eebc22c25",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            SupplierId = null,
            SupplierName = "Storebrand Forsikkring",
            SupplierBankAccount = "81013009090",
            Description = "Forsikkring",
            PaymentMethod = null,
            MonthlyAmount = -3481,
            MonthlyDeadlineDay = 20,
            FixedExpenseStartDate = DateOnly.FromDateTime(DateTime.Today),
            IsActive = true,
            StandardAccountNumberForePayment = "Regningskonto"
        };
        
        await app.AddFixedExpense(fixedExpenseOb);

        List<FixedExpenseOb> des = await app.GetFixedExpensesByUserId(fixedExpenseOb.UserId);

        foreach (var d in des)
        {
            Console.WriteLine(d.Description + " " + d.SupplierId + " " + d.SupplierBankAccount + " " + d.PaymentMethod);
        }
    }
}