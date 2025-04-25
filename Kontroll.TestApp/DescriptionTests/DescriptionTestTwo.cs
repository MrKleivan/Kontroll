using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.DescriptionTests;

public class DescriptionTestTwo
{
    public async Task Run(IConfiguration config)
    {
        TransactionController app = new TransactionController(config);

        TransactionPostRequest transaction = new TransactionPostRequest()
        {
            TransactionId = "harehoppdsvsdv",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,03,24),
            AccountNumber = "255 666",
            Description = "KIWI 555 AUSTBY TINN AUSTBYG TINN AUSTBYGD",
            Income = 20,
            Outcome = 0,
            ToAccount = "100",
            FromAccount = "200",
            IsFixedPayment = false,
            FixedPaymentId = null,
            ForceAdd = false,
        };
        
        await app.AddTransaction(transaction);
        
        Console.WriteLine(transaction.Description);
        
        TransactionOb newTransaction = new TransactionOb()
        {
            TransactionId = "harehoppdsvsdv",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,03,24),
            AccountNumber = "255 666",
            Description = "Banan",
            Income = 20,
            Outcome = 0,
            ToAccount = "100",
            FromAccount = "200",
            IsFixedPayment = false,
            FixedPaymentId = null,
        };

        TransactionOb trans = await app.GetSingleTransactionByTransactionId(newTransaction);
        
        Console.WriteLine(trans.Description);

    }
}