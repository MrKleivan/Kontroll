using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;

namespace Kontroll.Controller;

public class SupplierController
{
    private readonly SupplierDb _db;
    private readonly IConfiguration _configuration;
    
    public SupplierController(IConfiguration config)
    {
        _db = new SupplierDb(config);
        _configuration = config;
    }

    public async Task<bool> AddSupplier(SupplierOb supplierOb)
    {
        var IsExistingSupplier = await IsSupplierExisting(supplierOb);
        if (!IsExistingSupplier)
        {
            return await _db.AddSupplierToDatabase(supplierOb);
        }
        return false;
    }

    public async Task<bool> IsSupplierExisting(SupplierOb supplierOb)
    {
        return await _db.IsSupplierExistingInDatabase(supplierOb);
    }
    

    public async Task<List<SupplierOb>> GetAllSuppliersByUserId(Object obj)
    {
        return await _db.GetAllSuppliersByUserIdFromDatabase(obj);
    }

    public async Task<SupplierOb> GetSupplierBySupplierName(Object obj)
    {
        return await _db.GetSupplierFromDatabaseBySupplierName(obj);
    }

    public async Task<decimal> GetUsersBalanceSheetAtSupplier(object obj)
    {
        TransactionController transactionController = new TransactionController(_configuration);

        decimal income = 0;
        decimal outcome = 0;
        
        List<TransactionOb> transactionObs = await transactionController.GetTransactionBySupplierIdAndYear(obj);
        
        foreach (var transaction in transactionObs)
        {
            income += transaction.Income;
            outcome -= transaction.Outcome;
        }

        return income - outcome;
    }
}