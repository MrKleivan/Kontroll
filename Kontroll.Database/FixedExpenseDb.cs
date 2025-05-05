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

    public async Task<List<FixedExpenseOb>> GetAllFixedExpensesFromDatabaseByUserId(object obj)
    {
        var query = "SELECT * FROM FixedExpenses WHERE UserId = @UserId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, obj);
    }
    
    public async Task<List<FixedExpenseOb>> GetAllFixedExpensesFromDatabaseByFixedExpenseId(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT * FROM FixedExpenses WHERE FixedExpenseId = @FixedExpenseId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, fixedExpenseOb);
    }

    public async Task<bool> AddFixedExpenseToDatabase(FixedExpenseOb fixedExpenseOb)
    {
        var query = "INSERT INTO FixedExpenseTb ( UserId, SupplierId, Description, PaymentMethod, StandardAccountNumberForePayment, MonthlyAmount, MonthlyDeadlineDate, IsPaid, SettledDate, AmountPayed, AccountNumberUsedForePayment, TransactionId) " +
                    "VALUES (@UserId, @SupplierId, @Description, @PaymentMethod, @StandardAccountNumberForePayment, @MonthlyAmount, @MonthlyDeadlineDate, @IsPaid, @SettledDate, @AmountPayed, @AccountNumberUsedForePayment, @TransactionId)";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }

    public async Task<bool> UpdateFixedExpenseInDatabase(FixedExpenseOb fixedExpenseOb)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> FixedExpenseExistsInDatabase(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT COUNT(*) FROM FixedExpenseTb WHERE Description = @Description AND MonthlyAmount = @MonthlyAmount AND MonthlyDeadlineDate = @MonthlyDeadlineDate";

        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }
}