using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;


namespace Kontroll.Database.TableControllers;

public class BankAccountDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelper = new();
    private string _tableName = "BankAccountTb";
    
    public BankAccountDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }


    public async Task<bool> AddBankAccountToDatabase(BankAccountOb bankAccountOb)
    {
        var query = $"INSERT INTO {_tableName} (UserId, AccountNumber, Name, Type, Id) VALUES (@UserId, @AccountNumber, @Name, @Type, @Id)";

        return await _sqlReaderHelper.ExecuteNonQueryAsync(_connectionString, query, bankAccountOb) > 0;
    }

    public async Task<bool> UpdateBankAccountInDatabase(BankAccountDb db)
    {
        var query = $"UPDATE {_tableName} SET AccountNumber = @AccountNumber, Name = @Name, Type = @Type WHERE Id = @Id";
        
        return await _sqlReaderHelper.ExecuteNonQueryAsync(_connectionString, query, db) > 0;
    }

    public async Task<bool> DeleteBankAccountFromDatabase(BankAccountDb db)
    {
        var query = $"DELETE FROM {_tableName} WHERE Id = @Id";
        
        return await _sqlReaderHelper.ExecuteNonQueryAsync(_connectionString, query, db) > 0;
    }

    public async Task<List<BankAccountOb>> GetAllUsersBankAccountsFromDatabase(object obj)
    {
        var query = $"SELECT * FROM {_tableName} WHERE UserId = @UserId";
        
        return await _sqlReaderHelper.ExecuteReaderAndMapAsync<BankAccountOb>(_connectionString, query, obj);
    }
}