using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Library.Exel;
using Kontroll.Model;
using Transaction = System.Transactions.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class TransactionController : ControllerBase
{
    private readonly TransactionDb _db;
    private DescriptionController _descriptionController;
    private FixedExpenseController _fixedExpenseController;
    private SupplierController _supplierController;

    public TransactionController(IConfiguration config)
    {
        _db = new TransactionDb(config);
        _descriptionController = new DescriptionController(config);
        _fixedExpenseController = new FixedExpenseController(config);
        _supplierController = new SupplierController(config);
    }

    public async Task<TransactionOb> GetSingleTransactionByTransactionId(TransactionOb transaction)
    {
        return await _db.GetTransactionFromDatabaseByTransactionId(transaction);
    }

    public async Task<List<TransactionOb>> GetAllTransactionsByFixedExpenseId(FixedExpenseOb fixedExpenseOb, int Year)
    {
        object queryObj = new 
        {
            FixedExpenseId = fixedExpenseOb.FixedExpenseId,
            Year = Year
        };
        return await _db.GetAllTransactionsFromDatabaseByFixedExpenseId(queryObj);
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
        var exists = await TransactionExists(transaction);
        if (!exists || transaction.ForceAdd)
        {
            await UpdateValuesOnTransactionByCriteria(transaction);
            
            var added = await _db.AddTransactionToDatabase(await ConvertTransactionObject(transaction));
            return added ? Ok("Transaction added") : StatusCode(500, "Failed to add transaction");
        }
        return Conflict(new {message = "Transaction already exists. Do you want to continue?"});
    }

    private async Task UpdateValuesOnTransactionByCriteria(TransactionPostRequest transaction)
    {
            var existingDescription = await CheckIfTransactionHasOtherDescription(transaction);
            
            if (existingDescription)
            {
                DescriptionOb descriptionOb = await _descriptionController.GetDescriptionByExternalDescription(transaction);
                if (descriptionOb != null)
                {
                    transaction.UserDescription = descriptionOb.UserDescription;
                }
            }

            bool isFixedExpense = await _fixedExpenseController.CheckIfIsFixedExpense(await ConvertTransactionObject(transaction));

            if (isFixedExpense)
            {
                FixedExpenseOb? fixedExpenseOb = await _fixedExpenseController.GetFixedExpenseByDescriptionAndSupplierBankAccount(await ConvertTransactionObject(transaction));
                
                transaction.IsFixedExpense = true;
                transaction.FixedExpenseId = fixedExpenseOb.FixedExpenseId;
            }

            await CheckIfTransactionHasExistingSupplier(transaction);
    }

    private async Task CheckIfTransactionHasExistingSupplier(TransactionPostRequest transaction)
    {
        List<SupplierOb> supplierObs = await _supplierController.GetAllSuppliersByUserId(await ConvertTransactionObject(transaction));

        if (supplierObs.Count > 0)
        {
            foreach (SupplierOb supplierOb in supplierObs)
            {
                if (transaction.ExternalDescription != null && supplierOb.CompanyName != null && transaction.ExternalDescription.IndexOf(supplierOb.CompanyName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    transaction.SupplierId =  supplierOb.SupplierId;
                }
            }
        }
    }

    public async Task<List<TransactionPostRequest>?> AddUserDescriptionToTransaction(List<TransactionPostRequest> transactions)
    {
        List<TransactionPostRequest> transactionsFaildToAdd = new List<TransactionPostRequest>();

        foreach (var transaction in transactions)
        {
            var isTransactionUpdated = await _db.AddTransactionToDatabase(await ConvertTransactionObject(transaction));

            if (!isTransactionUpdated)
            {
                transactionsFaildToAdd.Add(transaction);
            }
        }
        return transactionsFaildToAdd.Count > 0 ? transactionsFaildToAdd : null;
    }
    
    public async Task<bool> CheckIfTransactionHasOtherDescription(TransactionPostRequest transaction)
    {
        return await _descriptionController.DescriptionExists(await ConvertTransactionObject(transaction));
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
                ExternalDescription = obj.Beskrivelse,
                UserDescription = null,
                Income = income,
                Outcome = outcom,
                ToAccount = obj.Tilkonto,
                FromAccount = obj.Frakonto,
                SupplierId = null,
                IsFixedExpense = false,
                FixedExpenseId = null,
                HasReceipt = false,
                ReceiptId = null,
                HasInvoice = false, 
                InvoiceId = null,
                ForceAdd = false,
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
            ExternalDescription = transaction.ExternalDescription,
            UserDescription = transaction.UserDescription,
            Income = transaction.Income,
            Outcome = transaction.Outcome,
            ToAccount = transaction.ToAccount,
            FromAccount = transaction.FromAccount,
            SupplierId = transaction.SupplierId,
            IsFixedExpense = transaction.IsFixedExpense,
            FixedExpenseId = transaction.FixedExpenseId,
            HasReceipt = transaction.HasReceipt,
            ReceiptId = transaction.ReceiptId,
            HasInvoice = transaction.HasInvoice,
            InvoiceId = transaction.InvoiceId, 
        };
        return transactionOb;
    }
}