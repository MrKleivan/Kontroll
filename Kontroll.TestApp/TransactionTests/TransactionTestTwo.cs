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
            TransactionId = "6eab5e0c-02a6-4d72-b7d5-c3b2a272314c",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,01,08),
            AccountNumber = "255 666",
            ExternalDescription = "KIWI 555 AUSTBY TINN AUSTBYG TINN AUSTBYGD",
            UserDescription = "Matvarer",
            Income = 0m,
            Outcome = -38.40m,
            ToAccount = "",
            FromAccount = "26102149598",
            IsFixedPayment = false,
            FixedPaymentId = null,
        };
        
        var transaction = await app.GetSingleTransactionByTransactionId(transactionOb);

        if (transaction != null)
        {
            Console.WriteLine($"✅ Fant transaksjon: {transaction.TransactionId} - {(transaction.UserDescription != null ? transaction.UserDescription : transaction.ExternalDescription)} ({transaction.Date})");
        }
        else
        {
            Console.WriteLine("❌ Fant ingen transaksjon med den ID-en.");
        }
    }
} 