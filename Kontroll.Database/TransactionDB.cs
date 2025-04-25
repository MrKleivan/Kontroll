using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Transactions;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Kontroll.Database;

public class TransactionDB
{
    private readonly string? _connectionString;
    private SqlParameterHelperDb _sqlParameterHelperDb = new SqlParameterHelperDb();
    private SqlReaderHelperDb _sqlReaderHelperDb = new SqlReaderHelperDb();

    public TransactionDB(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }
    
    
    public async Task<TransactionOb?> GetTransactionFromDatabaseByTransactionId(TransactionOb transaction)
    {
        SortRequest sortRequest = null;
        var query = "SELECT * FROM TransactionTb WHERE TransactionId = @TransactionId";
        
        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<TransactionOb>(_connectionString, query, transaction);
    }

    public async Task<List<TransactionOb>> GetTransactionsSortedByDate([FromBody] SortRequest sortRequest)
    {
        var query = $"SELECT * FROM TransactionTb WHERE {sortRequest.Request} BETWEEN @StartDate AND @EndDate";
        Console.WriteLine(sortRequest.StartDate);
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
    
    public async Task<List<TransactionOb>> GetTransactionsSortedByDescription([FromBody] SortRequest sortRequest)
    {
        var query = $"SELECT * FROM TransactionTb WHERE Description = @Description";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<TransactionOb>(_connectionString, query, sortRequest);
    }

    public async Task<bool> AddTransactionToDatabase(TransactionOb transaction)
    {
        var query = "INSERT INTO TransactionTb (TransactionId, UserId, Date, AccountNumber, Description, Income, Outcome, ToAccount, FromAccount, IsFixedPayment, FixedPaymentId)" 
                    + "VALUES (@TransactionId, @UserId, @Date, @AccountNumber, @Description, @Income, @Outcome, @ToAccount, @FromAccount, @IsFixedPayment ,@FixedPaymentId)";
        
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
        var query = @"UPDATE TransactionTb SET Date = @Date, AccountNumber = @AccountNumber, Description = @Description, Income = @Income, Outcome = @Outcome, ToAccount = @ToAccount, FromAccount = @FromAccount, IsFixedPayment = @IsFixedPayment, FixedPaymentId = @FixedPaymentId WHERE TransactionId = @TransactionId"; 
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, transaction) > 0;
    }
    
    
    public async Task<bool> TransactionExistsInDatabase(TransactionOb transaction)
    {
        SortRequest sortRequest = null;
        var query = @"SELECT COUNT(*) FROM TransactionTb WHERE Date = @Date AND AccountNumber = @AccountNumber AND Description = @Description AND Income = @Income AND Outcome = @Outcome";

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var sql = new SqlCommand(query, connection);
        _sqlParameterHelperDb.AddSqlParameterFromObject(sql, transaction, query);

        var result = (int)await sql.ExecuteScalarAsync();
        return result > 0;
    } 
    
}