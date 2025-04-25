using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database;

public class FixedExpenseDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb;

    public FixedExpenseDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("FixedExpensesDb")
            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<List<FixedExpenseOb>> GetAllFixedExpenses(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT * FROM FixedExpenses WHERE UserId = @UserId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, fixedExpenseOb);
    }
    
    public async Task<List<FixedExpenseOb>> GetAllFixedExpensesByFixedExpenseId(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT * FROM FixedExpenses WHERE FixedExpenseId = @FixedExpenseId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, fixedExpenseOb);
    }

    public async Task<bool> AddFixedExpense(FixedExpenseOb fixedExpenseOb)
    {
        return true;
    }
}