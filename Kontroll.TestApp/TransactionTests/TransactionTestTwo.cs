using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Model;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp;

public class TransactionTestTwo
{
    public async Task Run(IConfigurationRoot config)
    {
        TransactionController app = new TransactionController(config);
        
        TransactionOb transactionOb = new TransactionOb
        {
            TransactionId = "a657e1c5-b886-4409-b4dc-12a1cae200d6",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,01,27),
            AccountNumber = "255 666",
            Description = "Overføring fra kredittkort til konto",
            Income = 1000.00m,
            Outcome = 0.00m,
            ToAccount = "26102149598",
            FromAccount = "24809453517",
            IsFixedPayment = false,
            FixedPaymentId = null,
        };
        
        var transaction = await app.GetSingleTransactionByTransactionId(transactionOb);

        if (transaction != null)
        {
            Console.WriteLine($"✅ Fant transaksjon: {transaction.TransactionId} - {transaction.Description} ({transaction.Date})");
        }
        else
        {
            Console.WriteLine("❌ Fant ingen transaksjon med den ID-en.");
        }
    }
} 