using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp;

public class TransactionTestTwo
{
    public async Task Run(IConfigurationRoot config)
    {
        TransactionController app = new TransactionController(config);
        
        TransactionOb transactionOb = new TransactionOb
        {
            TransactionId = "c7716a4a-1ce7-46cf-a784-e6ee100d0eb3",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,01,08),
            AccountNumber = "255 666",
            ExternalDescription = "KIWI 555 AUSTBY TINN AUSTBYG TINN AUSTBYGD",
            UserDescription = "Matvarer",
            Income = 0m,
            Outcome = -38.40m,
            ToAccount = "",
            FromAccount = "26102149598",
            IsFixedExpense = false,
            FixedExpenseId = null,
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