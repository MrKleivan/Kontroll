using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class FixedTransactionController : ControllerBase
{
   
    private FixedTransactionDb _db;
    private IConfiguration _configuration;

    public FixedTransactionController(IConfiguration config)
    {
        _db = new FixedTransactionDb(config);
        _configuration = config;
    }
    
    public async Task<bool> AddFixedExpense(FixedTransactionsOb fixedTransactionsOb)
    {
        var fixedExpenseExists = await CheckIfFixedExpenseExists(fixedTransactionsOb);
        if (!fixedExpenseExists)
        {
            await CheckIfFixedExpenseHasExistingSupplier(fixedTransactionsOb);
            return await _db.AddFixedExpenseToDatabase(fixedTransactionsOb);
        }
        return false;
    }

    public async Task<bool> CheckIfIsFixedExpense(TransactionOb transactionOb)
    {
        bool isFixedExpense = await _db.IsFixedExpense(transactionOb);
        
        return isFixedExpense;
    }

    public async Task<bool> CheckIfFixedExpenseExists(FixedTransactionsOb fixedTransactionsOb)
    {
        var fixedExpenseExists = await _db.FixedExpenseExistsInDatabase(fixedTransactionsOb);
        
        return fixedExpenseExists;
    }

    public async Task<List<FixedTransactionsOb>> GetFixedExpensesByUserId(string userId)
    {
        return await _db.GetAllFixedExpensesFromDatabaseByUserId(userId);
    }

    public async Task<FixedTransactionsOb?> GetFixedExpenseByFixedExpenseId(FixedTransactionsOb fixedTransactionsOb)
    {
        return await _db.GetFixedExpenseFromDatabaseByFixedExpenseId(fixedTransactionsOb);
    }

    public async Task<FixedTransactionsOb?> GetFixedExpenseByDescriptionAndSupplierBankAccount(TransactionOb transactionOb)
    {
        return await _db.GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(transactionOb);
    }


    public async Task CheckIfFixedExpenseHasExistingSupplier(FixedTransactionsOb fixedTransactionsOb)
    {
        SupplierController supplierApp = new SupplierController(_configuration);
        SupplierOb supplier = await supplierApp.GetSupplierBySupplierName(fixedTransactionsOb);

        if (supplier != null)
        {
            fixedTransactionsOb.SupplierId = supplier.SupplierId;
        }
        
    }

    public async Task<bool> UpdateFixedExpense(FixedTransactionsOb fixedTransactionsOb)
    {
        var fixedExpenseExists = await CheckIfFixedExpenseExists(fixedTransactionsOb);

        if (fixedExpenseExists)
        {
            return await _db.UpdateFixedExpenseInDatabase(fixedTransactionsOb);
        }
        return false;
    }
    
    
}