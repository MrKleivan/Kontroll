using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class FixedTransactionDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb = new();

    public FixedTransactionDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<List<FixedTransactionsOb>> GetAllFixedExpensesFromDatabaseByUserId(string  UserId)
    {
        var query = "SELECT * FROM FixedTransactionTb WHERE UserId = @UserId";

        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<FixedTransactionsOb>(_connectionString, query, new { UserId });
    }
    
    public async Task<FixedTransactionsOb?> GetFixedExpenseFromDatabaseByFixedExpenseId(FixedTransactionsOb fixedTransactionsOb)
    {
        var query = "SELECT * FROM FixedTransactionTb WHERE FixedTransactionId = @FixedTransactionId";

        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<FixedTransactionsOb>(_connectionString, query, fixedTransactionsOb);
    }

    public async Task<bool> AddFixedExpenseToDatabase(FixedTransactionsOb fixedTransactionsOb)
    {
        var query = "INSERT INTO FixedTransactionsTb ( UserId, FixedTransactionId, SupplierId, SupplierName, SupplierBankAccount, Type, Description, PaymentMethod, MonthlyAmount, MonthlyDeadlineDay, FixedTransactionStartDate, IsActive, StandardAccountNumberForePayment) " +
                    "VALUES (@UserId, @FixedTransactionId, @SupplierId, @SupplierName, @SupplierBankAccount, @Type, @Description, @PaymentMethod, @MonthlyAmount, @MonthlyDeadlineDay, @FixedTransactionStartDate, @IsActive, @StandardAccountNumberForePayment)";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedTransactionsOb) > 0;
    }

    public async Task<bool> UpdateFixedExpenseInDatabase(FixedTransactionsOb fixedTransactionsOb)
    {
        var query = "UPDATE FixedTransactionTb SET UserId = @UserId, FixedTransactionId = @FixedTransactionId, SupplierId = @SupplierId, SupplierName = @SupplierName, SupplierBankAccount = @SupplierBankAccount, Type = @Type, Description = @Description, PaymentMethod = @PaymentMethod, MonthlyAmount = @MonthlyAmount, MonthlyDeadlineDay = @MonthlyDeadlineDay, FixedTransactionStartDate = @FixedTransactionStartDate, IsActive = @IsActive, StandardAccountNumberForePayment = @StandardAccountNumberForePayment WHERE FixedTransactionId = @FixedTransactionId";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedTransactionsOb) > 0;
    }

    public async Task<bool> FixedExpenseExistsInDatabase(FixedTransactionsOb fixedTransactionsOb) 
    {
        var query = "SELECT COUNT(*) FROM FixedTransactionTb WHERE Description = @Description AND MonthlyAmount = @MonthlyAmount AND MonthlyDeadlineDay = @MonthlyDeadlineDay";

        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedTransactionsOb) > 0;
    }

    public async Task<FixedTransactionsOb?> GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(TransactionOb transactionOb)
    {
        var query = "SELECT * FROM FixedTransactionTb WHERE Description = @ExternalDescription AND SupplierBankAccount =  @ToAccount";
        
        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<FixedTransactionsOb>(_connectionString, query, transactionOb);
    }
    
    public async Task<bool> IsFixedExpense(TransactionOb transactionOb)
    {
        return await GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(transactionOb) != null;
    }
}