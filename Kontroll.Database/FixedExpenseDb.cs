using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database;

public class FixedExpenseDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb = new SqlReaderHelperDb();

    public FixedExpenseDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<List<FixedExpenseOb>> GetAllFixedExpensesFromDatabaseByUserId(object obj)
    {
        var query = "SELECT * FROM FixedExpenseTb WHERE UserId = @UserId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, obj);
    }
    
    public async Task<List<FixedExpenseOb>> GetAllFixedExpensesFromDatabaseByFixedExpenseId(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT * FROM FixedExpenseTb WHERE FixedExpenseId = @FixedExpenseId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, fixedExpenseOb);
    }

    public async Task<bool> AddFixedExpenseToDatabase(FixedExpenseOb fixedExpenseOb)
    {
        var query = "INSERT INTO FixedExpenseTb ( UserId, FixedExpenseId, SupplierId, SupplierBankAccount, Description, PaymentMethod, MonthlyAmount, MonthlyDeadlineDay, HasPayments, IsFullyPaid, SettledDate, StandardAccountNumberForePayment) " +
                    "VALUES (@UserId, @FixedExpenseId, @SupplierId, @SupplierBankAccount, @Description, @PaymentMethod, @MonthlyAmount, @MonthlyDeadlineDay, @HasPayments, @IsFullyPaid, @SettledDate, @StandardAccountNumberForePayment)";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }

    public async Task<bool> UpdateFixedExpenseInDatabase(FixedExpenseOb fixedExpenseOb)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> FixedExpenseExistsInDatabase(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT COUNT(*) FROM FixedExpenseTb WHERE Description = @Description AND MonthlyAmount = @MonthlyAmount AND MonthlyDeadlineDay = @MonthlyDeadlineDay";

        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }

    public async Task<bool> IsFixedExpense(TransactionOb transactionOb)
    {
        var query = "SELECT * FROM FixedExpenseTb WHERE Description = @UserDescription";

        bool isFixedExpense = false;
        
        List<FixedExpenseOb?> fixedExpenseObs = await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedExpenseOb>(_connectionString, query, transactionOb);
        if (fixedExpenseObs.Count > 0)
        {
            FixedExpenseOb fixedExpenseOb = fixedExpenseObs.Find(t => t.SupplierBankAccount == transactionOb.ToAccount);
            isFixedExpense = fixedExpenseOb != null;
        }
        
        return isFixedExpense;
    }
}