using System.Globalization;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;
using Kontroll.Library.Exel;
using Microsoft.AspNetCore.Mvc;
namespace Kontroll.Controller;

public class TransactionController : ControllerBase
{
    private readonly TransactionDb _db;
    private readonly IConfiguration _configuration;

    public TransactionController(IConfiguration config)
    {
        _db = new TransactionDb(config);
        _configuration = config;
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
    
    public async Task<TransactionOb?> GetSingleTransactionByTransactionId(object transaction)
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
        else if (sortRequest.Request == "AccountNumber")
        {
            if (sortRequest.SupplierAccountNumber != "" && sortRequest.UserAccountNumber == "")
            {
                return await _db.GetTransactionsBySuppliersAccountNumber(sortRequest);
            }
            else if (sortRequest.SupplierAccountNumber == "" && sortRequest.UserAccountNumber != "")
            {
                return await _db.GetTransactionsByUserAccountNumber(sortRequest);
            }
            return await _db.GetTransactionsByUserAccountNumberAndSuppliersAccountNumber(sortRequest);
        }
        else return null;
    }

    public async Task<List<TransactionOb>> GetTransactionBySupplierIdAndYear(object obj)
    {
        List<TransactionOb> transactions = await _db.GetTransactionFromDatabaseBySupplierIdAndYear(obj);
        
        return transactions;
    }

    private async Task UpdateValuesOnTransactionByCriteria(TransactionPostRequest transaction)
    {
            var existingDescription = await CheckIfTransactionHasOtherDescription(transaction);
            
            if (existingDescription)
            {
                DescriptionController descriptionController = new DescriptionController(_configuration);
                DescriptionOb descriptionOb = await descriptionController.GetDescriptionByExternalDescription(transaction);
                if (descriptionOb != null)
                {
                    transaction.UserDescription = descriptionOb.UserDescription;
                }
            }
            
            FixedTransactionController fixedTransactionController = new FixedTransactionController(_configuration);
            bool isFixedExpense = await fixedTransactionController.CheckIfIsFixedExpense(await ConvertTransactionObject(transaction));

            if (isFixedExpense)
            {
                FixedTransactionsOb? fixedTransactionsOb = await fixedTransactionController.GetFixedExpenseByDescriptionAndSupplierBankAccount(await ConvertTransactionObject(transaction));
                
                transaction.IsFixedExpense = true;
                transaction.FixedTransactionId = fixedTransactionsOb.FixedTransactionId;
            }

            await CheckIfTransactionHasExistingSupplier(transaction);
    }

    private async Task CheckIfTransactionHasExistingSupplier(TransactionPostRequest transaction)
    {
        SupplierController supplierController = new SupplierController(_configuration);
        List<SupplierOb> supplierObs = await supplierController.GetAllSuppliersByUserId(await ConvertTransactionObject(transaction));

        if (supplierObs.Count > 0)
        {
            foreach (SupplierOb supplierOb in supplierObs)
            {
                if (transaction.ExternalDescription != null && supplierOb.SupplierName != null && transaction.ExternalDescription.IndexOf(supplierOb.SupplierName, StringComparison.OrdinalIgnoreCase) >= 0)
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
        DescriptionController descriptionController = new DescriptionController(_configuration);
        return await descriptionController.DescriptionExists(await ConvertTransactionObject(transaction));
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

    public async Task<List<TransactionPostRequest>> ConvertExelToObjectList(IFormFile file, [FromForm] string userId, [FromForm] string AccountNumber)
    {
        List<TransactionPostRequest> transactions = new List<TransactionPostRequest>();
        var fileObject = await Task.Run(() => Exel.ExelToObjects(file));

        foreach (dynamic obj in fileObject)
        {
            string CleanStr(string? s) => s?.Trim().Trim('"') ?? "";
            string NormalizeNumber(string? s) => CleanStr(s).Replace(" ", "").Replace(",", ".");

            var incomeStr = NormalizeNumber(obj.Inn);
            var outcomeStr = NormalizeNumber(obj.Ut);
            var datoStr = CleanStr(obj.Dato);

            decimal.TryParse(incomeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal income);
            decimal.TryParse(outcomeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal outcome);
            DateTime.TryParse(datoStr, out DateTime dt);
            var date = DateOnly.FromDateTime(dt);

            var transaction = new TransactionPostRequest
            {
                TransactionId = Guid.NewGuid().ToString(),
                UserId = userId,
                Date = date,
                AccountNumber = AccountNumber,
                ExternalDescription = CleanStr(obj.Beskrivelse),
                UserDescription = null,
                Income = income,
                Outcome = outcome,
                ToAccount = CleanStr(obj.Tilkonto),
                FromAccount = CleanStr(obj.Frakonto),
                SupplierId = null,
                IsFixedExpense = false,
                FixedTransactionId = null,
                HasReceipt = false,
                ReceiptId = null,
                HasInvoice = false,
                InvoiceId = null,
                ForceAdd = false,
            };
            await UpdateValuesOnTransactionByCriteria(transaction);
            transactions.Add(transaction);
        }
        return transactions;
    }

    public async Task<TransactionOb?> GetTransactionByInvoiceValues(InvoiceOb invoice)
    {
        return await _db.GetTransactionFormDatabaseByInvoiceValues(invoice);
    }
    public async Task<bool> TransactionExists(dynamic transaction)
    {
        TransactionOb transactionOb = await ConvertTransactionObject(transaction);
        return await _db.TransactionExistsInDatabase(transactionOb);
    }

    public async Task<TransactionOb> ConvertTransactionObject(dynamic transaction)
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
            FixedTransactionId = transaction.FixedTransactionId,
            HasReceipt = transaction.HasReceipt,
            ReceiptId = transaction.ReceiptId,
            HasInvoice = transaction.HasInvoice,
            InvoiceId = transaction.InvoiceId, 
        };
        return transactionOb;
    }
}