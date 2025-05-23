using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp;

public class TransactionTestThree
{
    public async Task Run(IConfigurationRoot config)
    {
        TransactionController app = new TransactionController(config);
     
        // Test 1
        
        SortRequest request = new SortRequest
        {
            UserId = null,
            TransactionId = null,
            Request = "Date",
            ExternalDescription = null,
            UserDescription = null,
            AccountNumber = null,
            MinAmount = 0m,
            MaxAmount = 0m,
            AmountDirection = null,
            StartDate = new DateOnly(2025, 01, 22),
            EndDate = new DateOnly(2025, 01, 24),
        };
        List<TransactionOb> transactionObOne = await app.GetUserTransactionsByCriteria(request);

        Console.WriteLine("------------Test1 sortering på Dato-------------");
        foreach (var transaction in transactionObOne)
        {
            Console.WriteLine($"{transaction.TransactionId} | {transaction.UserId} | {transaction.Date} | {(transaction.UserDescription != null ? transaction.UserDescription : transaction.ExternalDescription)}  | {transaction.AccountNumber} | {(double)transaction.Income} | {(double)transaction.Outcome}");
        }
        
        // Test 2

        request = new SortRequest
        {
            UserId = null,
            TransactionId = null,
            Request = "Amount",
            ExternalDescription = null,
            UserDescription = null,
            AccountNumber = null,
            MinAmount = 100m,
            MaxAmount = 2000m,
            AmountDirection = "Both",
            StartDate = null,
            EndDate = null,
        };
        
        List<TransactionOb> transactionObTwo = await app.GetUserTransactionsByCriteria(request);

        Console.WriteLine("------------Test2 sortering på Beløp-------------");
        foreach (var transaction in transactionObTwo)
        {
            Console.WriteLine($"{transaction.TransactionId} | {transaction.UserId} | {transaction.Date} | {(transaction.UserDescription != null ? transaction.UserDescription : transaction.ExternalDescription)}  | {transaction.AccountNumber} | {(double)transaction.Income} | {(double)transaction.Outcome}");
        }
    }
}