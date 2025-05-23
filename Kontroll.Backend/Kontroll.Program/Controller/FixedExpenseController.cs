using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class FixedExpenseController : ControllerBase
{
   
    private FixedExpenseDb _db;
    private IConfiguration _configuration;

    public FixedExpenseController(IConfiguration config)
    {
        _db = new FixedExpenseDb(config);
        _configuration = config;
    }
    
    public async Task<bool> AddFixedExpense(FixedExpenseOb fixedExpenseOb)
    {
        var fixedExpenseExists = await CheckIfFixedExpenseExists(fixedExpenseOb);
        if (!fixedExpenseExists)
        {
            await CheckIfFixedExpenseHasExistingSupplier(fixedExpenseOb);
            return await _db.AddFixedExpenseToDatabase(fixedExpenseOb);
        }
        return false;
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

    public async Task<List<FixedExpenseOb>> GetFixedExpensesByUserId(string userId)
    {
        return await _db.GetAllFixedExpensesFromDatabaseByUserId(userId);
    }

    public async Task<FixedExpenseOb?> GetFixedExpenseByFixedExpenseId(FixedExpenseOb fixedExpenseOb)
    {
        return await _db.GetFixedExpenseFromDatabaseByFixedExpenseId(fixedExpenseOb);
    }

    public async Task<FixedExpenseOb?> GetFixedExpenseByDescriptionAndSupplierBankAccount(TransactionOb transactionOb)
    {
        return await _db.GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(transactionOb);
    }


    public async Task CheckIfFixedExpenseHasExistingSupplier(FixedExpenseOb fixedExpenseOb)
    {
        SupplierController supplierApp = new SupplierController(_configuration);
        SupplierOb supplier = await supplierApp.GetSupplierBySupplierName(fixedExpenseOb);

        if (supplier != null)
        {
            fixedExpenseOb.SupplierId = supplier.SupplierId;
        }
        
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