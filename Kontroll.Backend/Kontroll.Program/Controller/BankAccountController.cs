using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;

namespace Kontroll.Controller;

public class BankAccountController
{
    private readonly BankAccountDb _db;

    public BankAccountController(IConfiguration config)
    {
        _db = new BankAccountDb(config);
    }

    public async Task<bool> AddBankAccount(BankAccountOb bankAccountOb)
    {
        if (bankAccountOb.Id == null || bankAccountOb.Id == "")
        {
            bankAccountOb.Id = Guid.NewGuid().ToString();
        }
        return await _db.AddBankAccountToDatabase(bankAccountOb);
    }

    public async Task<bool> DeleteBankAccount(BankAccountDb db)
    {
        return await _db.DeleteBankAccountFromDatabase(db);
    }

    public async Task<bool> UpdateBankAccount(BankAccountDb db)
    {
        return await _db.UpdateBankAccountInDatabase(db);
    }

    public async Task<List<BankAccountOb>> GetAllUsersBankAccounts(object obj)
    {
        return await _db.GetAllUsersBankAccountsFromDatabase(obj);
    }
}