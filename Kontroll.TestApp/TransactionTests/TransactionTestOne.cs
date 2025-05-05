using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp;

public class TransactionTestOne
{
    public async Task Run(IConfigurationRoot config)
    {
        var userId = "1e21c816-5591-40ca-b418-fd4c7c8ef188";
        string transactionId = Guid.NewGuid().ToString();
        string filePath = "test.csv";
        string accountNumber = "255 666";
        TransactionController app = new TransactionController(config);
        List<TransactionPostRequest> trasnactions = await app.ConvertExelToObjectList(filePath, userId, accountNumber);

        // List<TransactionOb> trasnactions = await controller.ConvertExelToObjectList(filePath, transactionId, accountNumber);

        if (trasnactions.Count > 0)
        {
            foreach (var transaction in trasnactions)
            {
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
        }
    }
}