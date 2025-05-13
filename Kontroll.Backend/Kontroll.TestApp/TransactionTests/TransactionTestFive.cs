using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Kontroll.TestApp;

public class TransactionTestFive
{
    public async Task Run(IConfigurationRoot config)
    {
        TransactionController app = new TransactionController(config);
        
        // test for å oppdatere en eksisterende transaksjon transactionId: 0fd3a267-e703-49fc-b40a-d01283650460 
        TransactionPostRequest transaction = new TransactionPostRequest()
        {
            TransactionId = "f496e0eb-b40e-457a-be07-1e8de34d1125",
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            Date = new DateOnly(2025,01,21),
            AccountNumber = "255 666",
            ExternalDescription = "STOREBRAND FORSIKRING AS",
            UserDescription = null,
            Income = 0,
            Outcome = -1891,
            ToAccount = "81013009090",
            FromAccount = "26102001612",
            IsFixedExpense = false,
            FixedExpenseId = null,
            ForceAdd = false,
        };
        
        TransactionOb newTransaction = new TransactionOb()
        {
            TransactionId = "f496e0eb-b40e-457a-be07-1e8de34d1125",
            
        };

        var response = await app.AddTransaction(transaction);
        
        if (response is ConflictObjectResult conflict)
        {
            Console.WriteLine(conflict.Value);
            var input = Console.ReadLine();

            if (input?.ToLower() == "j")
            {
                transaction.ForceAdd = true;
                var retry = await app.AddTransaction(transaction);
                Console.WriteLine(((ObjectResult)retry).Value);
            }
            else if (response is OkObjectResult ok)
            {
                Console.WriteLine(ok.Value);
            }
            else
            {
                Console.WriteLine("ukjent respons");
            }
        }
        
        TransactionOb findTransaction = await app.GetSingleTransactionByTransactionId(newTransaction);
        
        Console.WriteLine($"{findTransaction.TransactionId} |  {(double)findTransaction.Income} | {(findTransaction.UserDescription != null ? findTransaction.UserDescription : findTransaction.ExternalDescription)}");
    }
}