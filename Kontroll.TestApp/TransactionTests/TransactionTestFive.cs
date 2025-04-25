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
            TransactionId = "harehopp-92",
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
            ForceAdd = false,
        };
        
        TransactionOb newTransaction = new TransactionOb()
        {
            TransactionId = "harehopp-92",
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
        
        Console.WriteLine($"{findTransaction.TransactionId} |  {(double)findTransaction.Income} | {findTransaction.Description}");
    }
}