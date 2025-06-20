using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;

namespace Kontroll.Controller;

public class BankReconciliationController
{
    private readonly BankReconciliationDb _db;
    
    public BankReconciliationController(IConfiguration config)
    {
        _db = new BankReconciliationDb(config);
    }
    
    public async Task<bool> AddBankReconciliation(BankReconciliationOb bankReconciliationOb)
    {
        if (bankReconciliationOb.Id == null || bankReconciliationOb.Id == "")
        {
            bankReconciliationOb.Id = Guid.NewGuid().ToString();
        }
        return await _db.AddBankReconciliationToDatabase(bankReconciliationOb);
    }

    public async Task<bool> DeleteBankReconciliation(BankReconciliationOb bankReconciliationOb)
    {
        return await _db.DeleteBankReconciliationFromDatabase(bankReconciliationOb);
    }

    public async Task<bool> UpdateBankReconciliation(BankReconciliationOb bankReconciliationOb)
    {
        return await _db.UpdateBankReconciliationInDatabase(bankReconciliationOb);
    }

}