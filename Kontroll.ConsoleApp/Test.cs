using Kontroll.Controller;
using Kontroll.Model;
using System;


namespace Kontroll.ConsoleApp;

public class Test
{
    public async Task Run()
    {
        string transactionId = Guid.NewGuid().ToString();
        string filePath = "test.csv";
        string accountNumber = "255 666";
        TransactionController app = new TransactionController();
        List<TransactionOb> trasnactions = await app.ConvertExelToObjectList(filePath, transactionId, accountNumber);

        // List<TransactionOb> trasnactions = await controller.ConvertExelToObjectList(filePath, transactionId, accountNumber);

        if (trasnactions.Count > 0)
        {
            foreach (var transaction in trasnactions)
            {
                System.Console.WriteLine(transaction.TransactionId + "\t"
                                                            + transaction.Date + "\t"
                                                            + transaction.Description
                                                            + "\t" + transaction.Income
                                                            + "\t" + transaction.Outcome + "\t"
                                                            + transaction.ToAccount + "\t"
                                                            + transaction.FromAccount);

                // TransactionPostRequest trans = new TransactionPostRequest
                // {
                //     TransactionId = transaction.TransactionId,
                //     UserId = transaction.UserId,
                //     Date = transaction.Date,
                //     AccountNumber = transaction.AccountNumber,
                //     Description = transaction.Description,
                //     Income = transaction.Income,
                //     Outcome = transaction.Outcome,
                //     ToAccount = transaction.ToAccount,
                //     FromAccount = transaction.FromAccount,
                //     IsFixedPayment = transaction.IsFixedPayment,
                //     FixedPaymentId = transaction.FixedPaymentId,
                // };
                //
                // await app.AddTransactionToDatabase(trans);


            }
        }
    }

}