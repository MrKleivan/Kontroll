using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp;

public class TransactionTestFoure
{
    public async Task Run(IConfigurationRoot config)
    {
        TransactionController app = new TransactionController(config);
        
        // test for å oppdatere en eksisterende transaksjon transactionId: 8d3f6698-c9be-4058-8cdd-ceef688d52d2 
        TransactionOb transaction = new TransactionOb
        {
            TransactionId = "8d3f6698-c9be-4058-8cdd-ceef688d52d2",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,01,31),
            AccountNumber = "255 666",
            ExternalDescription = "Overføring fra kredittkort til konto",
            UserDescription = null,
            Income = 600,
            Outcome = 0,
            ToAccount = "26102149598",
            FromAccount = "24809453517",
            IsFixedPayment = false,
            FixedPaymentId = null,
        };
        
        var transactionBefore = await app.GetSingleTransactionByTransactionId(transaction);

        transaction.Outcome = transactionBefore.Outcome + 1;
        Console.WriteLine((double)transactionBefore.Outcome);
        await app.UpdateTransaction(transaction);
        var transactionNow = await app.GetSingleTransactionByTransactionId(transaction);

        Console.WriteLine((double)transactionNow.Outcome);
    }
}