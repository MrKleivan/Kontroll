using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Library.Exel;
using Kontroll.Model;
using Transaction = System.Transactions.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class TransactionController : ControllerBase
{
    private readonly TransactionDB _db;
    private DescriptionController _descriptionController;

    public TransactionController(IConfiguration config)
    {
        _db = new TransactionDB(config);
        _descriptionController = new DescriptionController(config);
    }

    public async Task<TransactionOb> GetSingleTransactionByTransactionId(TransactionOb transaction)
    {
        return await _db.GetTransactionFromDatabaseByTransactionId(transaction);
    }
    
    public async Task<List<TransactionOb>> GetUserTransactionsByCriteria([FromBody] SortRequest sortRequest)
    {
        if (sortRequest.Request == "Date")
        {
            return await _db.GetTransactionsSortedByDate(sortRequest);
        }
        else if (sortRequest.Request == "Description")
        {
            return await _db.GetTransactionsSortedByDescription(sortRequest);
        }
        else if (sortRequest.Request == "Amount")
        {
            return await _db.GetTransactionsSortedByAmount(sortRequest);
        }
        else return null;
    }

    public async Task<IActionResult> AddTransaction([FromBody] TransactionPostRequest transaction)
    {
        TransactionPostRequest hei = transaction;
        var exists = await TransactionExists(transaction);
        if (!exists || transaction.ForceAdd)
        {
            var existingDescription = await _descriptionController.DescriptionExists(await ConvertTransactionObject(transaction));
            
            if (existingDescription)
            {
                DescriptionOb descriptionOb = await _descriptionController.GetDescriptionByStandarDescription(transaction);
                hei.Description = descriptionOb.UsersDescription;
            }
            
            var added = await _db.AddTransactionToDatabase(await ConvertTransactionObject(hei));
            return added ? Ok("Transaction added") : StatusCode(500, "Failed to add transaction");
        }
        return Conflict(new {message = "Transaction already exists. Do you want to continue?"});
    }


    public async Task<bool> DeleteTransaction([FromBody] TransactionOb transaction)
    {
        var exists = await TransactionExists(transaction);

        if (exists)
        {
            return await _db.DeleteTransactionFromDatabase(transaction.TransactionId);
        }
        
        return false;
    }

    public async Task<bool?> UpdateTransaction([FromBody] TransactionOb transaction)
    {
        var exists = await TransactionExists(transaction);

        if (!exists)
        {
            return await _db.UpdateTransactionInDatabase(transaction);
        }

        return false;
    }

    public async Task<List<TransactionPostRequest>> ConvertExelToObjectList(string file, string userId, string AccountNumber)
    {
        List<TransactionPostRequest> transactions = new List<TransactionPostRequest>();
        var fileObject = await Task.Run(() => Exel.ExelToObjects(file));

        foreach (dynamic obj in fileObject)
        {
            decimal.TryParse((string)obj.Inn, out var income);
            decimal.TryParse((string)obj.Ut, out var outcom);
            DateOnly.TryParse((string)obj.Dato, out var date);
            var transaction = new TransactionPostRequest()
            {
                TransactionId = Guid.NewGuid().ToString(),
                UserId = userId,
                Date = date,
                AccountNumber = AccountNumber,
                Description = obj.Beskrivelse,
                Income = income,
                Outcome = outcom,
                ToAccount = obj.Tilkonto,
                FromAccount = obj.Frakonto,
                ForceAdd = false
            };
            transactions.Add(transaction);
        }
        return transactions;
    }

    private async Task<bool> TransactionExists(dynamic transaction)
    {
        TransactionOb transactionOb = await ConvertTransactionObject(transaction);
        return await _db.TransactionExistsInDatabase(transactionOb);
    }

    private async Task<TransactionOb> ConvertTransactionObject(dynamic transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        
        TransactionOb transactionOb = new TransactionOb
        {
            TransactionId = transaction.TransactionId,
            UserId = transaction.UserId,
            Date = transaction.Date,
            AccountNumber = transaction.AccountNumber,
            Description = transaction.Description,
            Income = transaction.Income,
            Outcome = transaction.Outcome,
            ToAccount = transaction.ToAccount,
            FromAccount = transaction.FromAccount
        };
        return transactionOb;
    }
}