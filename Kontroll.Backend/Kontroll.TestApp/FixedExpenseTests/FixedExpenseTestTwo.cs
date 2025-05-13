using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.FixedExpenseTests;

public class FixedExpenseTestTwo
{
    public async Task Run(IConfiguration config)
    {
        TransactionController app = new TransactionController(config);
        
        FixedExpenseOb fixedExpenseOb = new FixedExpenseOb
        {
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            FixedExpenseId = "7896f9c6-493e-441e-893c-f89eebc22c25",
            SupplierId = null,
            SupplierName = null,
            SupplierBankAccount = "81013009090",
            Description = "Forsikkring",
            PaymentMethod = null,
            MonthlyAmount = -3481,
            MonthlyDeadlineDay = 20,
            FixedExpenseStartDate = new DateOnly(),
            IsActive = true,
            StandardAccountNumberForePayment = "Regningskonto"
        };
        

        List<TransactionOb> des = await app.GetAllTransactionsByFixedExpenseId(fixedExpenseOb, 2025);

        foreach (var d in des)
        {
            Console.WriteLine(d.UserDescription + " " + d.FixedExpenseId + " " + d.Outcome + " " + d.Date);
        }
    }
}