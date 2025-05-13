using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class FixedExpenseDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb = new();

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
    
    public async Task<FixedExpenseOb?> GetFixedExpenseFromDatabaseByFixedExpenseId(FixedExpenseOb fixedExpenseOb)
    {
        var query = "SELECT * FROM FixedExpenseTb WHERE FixedExpenseId = @FixedExpenseId";

        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<FixedExpenseOb>(_connectionString, query, fixedExpenseOb);
    }

    public async Task<bool> AddFixedExpenseToDatabase(FixedExpenseOb fixedExpenseOb)
    {
        var query = "INSERT INTO FixedExpenseTb ( UserId, FixedExpenseId, SupplierId, SupplierName, SupplierBankAccount, Description, PaymentMethod, MonthlyAmount, MonthlyDeadlineDay, FixedExpenseStartDate, IsActive, StandardAccountNumberForePayment) " +
                    "VALUES (@UserId, @FixedExpenseId, @SupplierId, @SupplierName, @SupplierBankAccount, @Description, @PaymentMethod, @MonthlyAmount, @MonthlyDeadlineDay, @FixedExpenseStartDate, @IsActive, @StandardAccountNumberForePayment)";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }

    public async Task<bool> UpdateFixedExpenseInDatabase(FixedExpenseOb fixedExpenseOb)
    {
        var query = "UPDATE FixedExpenseTb SET UserId = @UserId, FixedExpenseId = @FixedExpenseId, SupplierId = @SupplierId, SupplierName = @SupplierName, SupplierBankAccount = @SupplierBankAccount, Description = @Description, PaymentMethod = @PaymentMethod, MonthlyAmount = @MonthlyAmount, MonthlyDeadlineDay = @MonthlyDeadlineDay, FixedExpenseStartDate = @FixedExpenseStartDate, IsActive = @IsActive, StandardAccountNumberForePayment = @StandardAccountNumberForePayment WHERE FixedExpenseId = @FixedExpenseId";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }

    public async Task<bool> FixedExpenseExistsInDatabase(FixedExpenseOb fixedExpenseOb) 
    {
        var query = "SELECT COUNT(*) FROM FixedExpenseTb WHERE Description = @Description AND MonthlyAmount = @MonthlyAmount AND MonthlyDeadlineDay = @MonthlyDeadlineDay";

        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, fixedExpenseOb) > 0;
    }

    public async Task<FixedExpenseOb?> GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(TransactionOb transactionOb)
    {
        var query = "SELECT * FROM FixedExpenseTb WHERE Description = @UserDescription AND SupplierBankAccount =  @ToAccount";
        
        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<FixedExpenseOb>(_connectionString, query, transactionOb);
    }
    
    public async Task<bool> IsFixedExpense(TransactionOb transactionOb)
    {
        return await GetFixedExpenseFromDatabaseByDescriptionAndSupplierBankAccount(transactionOb) != null;
    }
}