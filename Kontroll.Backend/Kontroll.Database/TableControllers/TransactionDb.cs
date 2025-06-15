using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class TransactionDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb = new();

    public TransactionDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }
    
    
    public async Task<TransactionOb?> GetTransactionFromDatabaseByTransactionId(object transaction)
    {
        string query = "SELECT * FROM TransactionTb WHERE TransactionId = @TransactionId";
        
        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<TransactionOb>(_connectionString, query, transaction);
    }

    public async Task<List<TransactionOb>> GetTransactionsSortedByDate([FromBody] SortRequest sortRequest)
    {
        var query = $"SELECT * FROM TransactionTb WHERE UserId = @UserId AND Date BETWEEN @StartDate AND @EndDate ORDER BY Date ASC";
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, sortRequest);
    }
    
    public async Task<List<TransactionOb>> GetTransactionsSortedByAmount([FromBody] SortRequest sortRequest)
    {
        string direction = "Both";
        var query = "";
        if (sortRequest.AmountDirection != "Both")
        {
            if (sortRequest.AmountDirection == "Inn")
            {
                direction = "Inncome";
            }
            else if (sortRequest.AmountDirection == "Out")
            {
                direction = "Outcome";
            }
            query = $"SELECT * FROM TransactionTb WHERE {direction} BETWEEN @MinAmount AND @MaxAmount";
        }
        else
        {
            query = "SELECT * FROM TransactionTb WHERE Income BETWEEN @MinAmount AND @MaxAmount OR Outcome BETWEEN -@MaxAmount AND @MinAmount";
        }
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, sortRequest);
    }
    
    public async Task<List<TransactionOb>> GetTransactionsSortedByDescription(object sortRequest)
    {
        var query = $"SELECT * FROM TransactionTb WHERE Description = @Description";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, sortRequest);
    }

    public async Task<bool> AddTransactionToDatabase(TransactionOb transaction)
    {
        var query = "INSERT INTO TransactionTb (TransactionId, UserId, Date, AccountNumber, ExternalDescription, UserDescription, Income, Outcome, ToAccount, FromAccount, SupplierId, IsFixedExpense, FixedExpenseId, HasReceipt, ReceiptId, HasInvoice, InvoiceId)" 
                    + "VALUES (@TransactionId, @UserId, @Date, @AccountNumber, @ExternalDescription, @UserDescription, @Income, @Outcome, @ToAccount, @FromAccount, @SupplierId ,@IsFixedExpense ,@FixedExpenseId, @HasReceipt, @ReceiptId, @HasInvoice, @InvoiceId)";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, transaction) > 0;
    }

    public async Task<bool> DeleteTransactionFromDatabase(string transactionId)
    {
        var transaction = new TransactionOb {TransactionId = transactionId};
        var query = "DELETE FROM TransactionTb WHERE TransactionId = @TransactionId";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, transaction) > 0;
    }

    public async Task<bool> UpdateTransactionInDatabase(TransactionOb transaction)
    {
        var query = @"UPDATE TransactionTb SET Date = @Date, AccountNumber = @AccountNumber, ExternalDescription = @ExternalDescription, UserDescription = @UserDescription, Income = @Income, Outcome = @Outcome, ToAccount = @ToAccount, FromAccount = @FromAccount, SupplierId = @SupplierId, IsFixedExpense = @IsFixedExpense, FixedTransactionId = @FixedTransactionId, HasReceipt = @HasReceipt, ReceiptId = @ReceiptId, HasInvoice = @HasInvoice, InvoiceId = @InvoiceId WHERE TransactionId = @TransactionId"; 
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, transaction) > 0;
    }
    
    
    public async Task<bool> TransactionExistsInDatabase(Object obj)
    {
        SortRequest sortRequest = null;
        var query = @"SELECT COUNT(*) FROM TransactionTb WHERE Date = @Date AND AccountNumber = @AccountNumber AND ExternalDescription = @ExternalDescription AND Income = @Income AND Outcome = @Outcome AND ToAccount = @ToAccount";
        
        var result = await _sqlReaderHelperDb.ExecuteScalarAsync<int>(_connectionString, query, obj);
        return result > 0;
    }

    public async Task<List<TransactionOb>> GetAllTransactionsFromDatabaseByFixedExpenseId(object queryObj)
    {
        var query = @"SELECT * FROM TransactionTb WHERE FixedTransactionId = @FixedTransactionId AND YEAR([Date]) = @Year";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, queryObj);
    }

    public async Task<TransactionOb?> GetTransactionFormDatabaseByInvoiceValues(InvoiceOb invoice)
    {
        var query = @"SELECT * FROM TransactionTb WHERE MONTH(Date) = MONTH(@PaymentDate) AND YEAR(Date) = YEAR(@PaymentDate) AND AccountNumber = @PayedFromAccountNumber AND Description = @Description AND Outcome = @PaymentAmount AND ToAccount = @SupplierBankAccountNumber";

        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<TransactionOb>(_connectionString, query, invoice);
    }

    public async Task<List<TransactionOb>> GetTransactionsBySuppliersAccountNumber(SortRequest sortRequest)
    {
        var query = @"SELECT * FROM TransactionTb WHERE ToAccount = @SupplierAccountNumber Or FromAccount = @SupplierAccountNumber";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, sortRequest);
    }

    public async Task<List<TransactionOb>> GetTransactionsByUserAccountNumber(SortRequest sortRequest)
    {
        var query = @"SELECT * FROM TransactionTb WHERE AccountNumber = @UserAccountNumber";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, sortRequest);
    }

    public async Task<List<TransactionOb>> GetTransactionsByUserAccountNumberAndSuppliersAccountNumber(SortRequest sortRequest)
    {
        List<TransactionOb> transactionObs = await GetTransactionsByUserAccountNumber(sortRequest);
        
        List<TransactionOb> transactions = await GetTransactionsBySuppliersAccountNumber(sortRequest);

        foreach (var transaction in transactions)
        {
            transactionObs.Add(transaction);
        }
        
        return transactionObs;
    }

    public async Task<List<TransactionOb>> GetTransactionFromDatabaseBySupplierIdAndYear(object obj)
    {
        var query = @"SELECT * FROM TransactionTb WHERE UserId = @UserId AND SupplierId = @SupplierId AND Year(Date) = @Year";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, obj);
    }
}