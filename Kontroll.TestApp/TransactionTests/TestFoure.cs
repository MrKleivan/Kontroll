using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp;

public class TestFoure
{
    public async Task Run(IConfigurationRoot config)
    {
        TransactionController app = new TransactionController(config);
        
        // test for å oppdatere en eksisterende transaksjon transactionId: 0fd3a267-e703-49fc-b40a-d01283650460 
        TransactionOb transaction = new TransactionOb
        {
            TransactionId = "0fd3a267-e703-49fc-b40a-d01283650460",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,01,24),
            AccountNumber = "255 666",
            Description = "Per Ivar Kleivan",
            Income = 0,
            Outcome = 76,
            ToAccount = "26102969730",
            FromAccount = "26102149598",
            IsFixedPayment = false,
            FixedPaymentId = null,
        };
        
        var transactionBefore = await app.GetSingleTransactionByTransactionId(transaction);

        transaction.Outcome = transactionBefore.Outcome + 1;
        Console.WriteLine((double)transactionBefore.Outcome);
        await app.UpdateTransactionInDatabase(transaction);
        var transactionNow = await app.GetSingleTransactionByTransactionId(transaction);

        Console.WriteLine((double)transactionNow.Outcome);
    }
}