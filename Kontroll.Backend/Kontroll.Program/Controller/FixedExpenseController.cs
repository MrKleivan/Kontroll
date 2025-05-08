using System.Transactions;
using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class FixedExpenseController : ControllerBase
{
   
    private FixedExpenseDb _db;
    private SupplierController _supplierController;
    private TransactionController _transactionController;

    public FixedExpenseController(IConfiguration config)
    {
        _db = new FixedExpenseDb(config);
        _supplierController = new SupplierController(config);
        _transactionController = new TransactionController(config);
    }

    public async Task<bool> CheckIfIsFixedExpense(TransactionOb transactionOb)
    {
        bool isFixedExpense = await _db.IsFixedExpense(transactionOb);
        
        return isFixedExpense;
    }

    public async Task<bool> CheckIfFixedExpenseExists(FixedExpenseOb fixedExpenseOb)
    {
        var fixedExpenseExists = await _db.FixedExpenseExistsInDatabase(fixedExpenseOb);
        
        return fixedExpenseExists;
    }

    public async Task<List<FixedExpenseOb>> GetFixedExpensesByUserId(FixedExpenseOb fixedExpenseOb)
    {
        return await _db.GetAllFixedExpensesFromDatabaseByUserId(fixedExpenseOb);
    }

    public async Task<FixedExpenseOb?> GetFixedExpenseByFixedExpenseId(FixedExpenseOb fixedExpenseOb)
    {
        return await _db.GetFixedExpenseFromDatabaseByFixedExpenseId(fixedExpenseOb);
    }

    public async Task<FixedExpenseOb?> GetFixedExpenseByDescriptionAndSupplierBankAccount(TransactionOb transactionOb)
    {
        return await _db.GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(transactionOb);
    }

    public async Task<bool> AddFixedExpense(FixedExpenseOb fixedExpenseOb)
    {
        var fixedExpenseExists = await CheckIfFixedExpenseExists(fixedExpenseOb);
        if (!fixedExpenseExists)
        {
            // await CheckIfFixedExpenseHasExistingSupplier(fixedExpenseOb);
            return await _db.AddFixedExpenseToDatabase(fixedExpenseOb);
        }
        return false;
    }

    public async Task<FixedExpenseOb?> CheckIfFixedExpenseHasExistingSupplier(FixedExpenseOb fixedExpenseOb)
    {
        List<TransactionOb> transactionObs = await _transactionController.GetAllTransactionsByFixedExpenseId(fixedExpenseOb, 2024);
        TransactionOb? transactionOb = transactionObs.FirstOrDefault();
        if (transactionOb != null && transactionOb.SupplierId != null)
        {
            fixedExpenseOb.SupplierId = transactionOb.SupplierId;
        }
        
        return fixedExpenseOb;
    }

    public async Task<bool> UpdateFixedExpense(FixedExpenseOb fixedExpenseOb)
    {
        var fixedExpenseExists = await CheckIfFixedExpenseExists(fixedExpenseOb);

        if (fixedExpenseExists)
        {
            return await _db.UpdateFixedExpenseInDatabase(fixedExpenseOb);
        }
        return false;
    }
    
    
}