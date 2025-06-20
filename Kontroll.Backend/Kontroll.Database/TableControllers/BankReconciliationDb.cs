using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class BankReconciliationDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelper = new();
    private string _tableName = "BankReconciliationTb";

    public BankReconciliationDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");;
    }


    public async Task<bool> AddBankReconciliationToDatabase(BankReconciliationOb bankReconciliationOb)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteBankReconciliationFromDatabase(BankReconciliationOb bankReconciliationOb)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateBankReconciliationInDatabase(BankReconciliationOb bankReconciliationOb)
    {
        throw new NotImplementedException();
    }
}