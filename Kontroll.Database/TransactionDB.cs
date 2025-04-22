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

    public TransactionDB(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }
    
    
    public async Task<TransactionOb?> GetTransactionFromDatabaseByTransactionId(TransactionOb transaction)
    {
        SortRequest sortRequest = null;
        var query = "SELECT * FROM TransactionTb WHERE TransactionId = @TransactionId";
        
        return await ExecuteReaderAndMapTransaction(transaction, query);
    }

    public async Task<List<TransactionOb>> GetMultipleTransactionsSortedByDate([FromBody] SortRequest sortRequest)
    {
        var query = $"SELECT * FROM TransactionTb WHERE {sortRequest.Request} BETWEEN @StartDate AND @EndDate";
        Console.WriteLine(sortRequest.StartDate);
        return await ExecuteReaderAndMapMultipleTransactions(sortRequest, query);
    }
    
    public async Task<List<TransactionOb>> GetMultipleTransactionsSortedByAmount([FromBody] SortRequest sortRequest)
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
        
        return await ExecuteReaderAndMapMultipleTransactions(sortRequest, query);
    }
    
    public async Task<List<TransactionOb>> GetTransactionsSortedByDescription([FromBody] SortRequest sortRequest)
    {
        var query = $"SELECT * FROM TransactionTb WHERE Description = @Description";
        
        return await ExecuteReaderAndMapMultipleTransactions(sortRequest, query);
    }

    public async Task<bool> AddTransactionToDatabase(TransactionOb transaction)
    {
        var query = "INSERT INTO TransactionTb (TransactionId, UserId, Date, AccountNumber, Description, Income, Outcome, ToAccount, FromAccount, IsFixedPayment, FixedPaymentId)" 
                    + "VALUES (@TransactionId, @UserId, @Date, @AccountNumber, @Description, @Income, @Outcome, @ToAccount, @FromAccount, @IsFixedPayment ,@FixedPaymentId)";
        
        return await ExecuteNonQuery(transaction, query);
    }

    public async Task<bool> DeleteTransactionFromDatabase(string transactionId)
    {
        var transaction = new TransactionOb {TransactionId = transactionId};
        var query = "DELETE FROM TransactionTb WHERE TransactionId = @TransactionId";
        
        return await ExecuteNonQuery(transaction, query);
    }

    public async Task<bool> UpdateTransactionInDatabase(TransactionOb transaction)
    {
        var query = @"UPDATE TransactionTb SET Date = @Date, AccountNumber = @AccountNumber, Description = @Description, Income = @Income, Outcome = @Outcome, ToAccount = @ToAccount, FromAccount = @FromAccount, IsFixedPayment = @IsFixedPayment, FixedPaymentId = @FixedPaymentId WHERE TransactionId = @TransactionId"; 
        return await ExecuteNonQuery(transaction, query);
    }
    
    
    public async Task<bool> TransactionExists(TransactionOb transaction)
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

    private async Task<bool> ExecuteNonQuery(TransactionOb transaction, string query)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        
        _sqlParameterHelperDb.AddSqlParameterFromObject(sql, transaction, query);
        
        return await sql.ExecuteNonQueryAsync() > 0;
    }

    private async Task<TransactionOb> ExecuteReaderAndMapTransaction(TransactionOb transaction, string query)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        _sqlParameterHelperDb.AddSqlParameterFromObject(sql, transaction, query);
        
        using var reader = await sql.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return AddValueFromReader(reader);
        }

        return null;
    }
    
    private async Task<List<TransactionOb>> ExecuteReaderAndMapMultipleTransactions(SortRequest sortRequest, string query)
    {
        var paramAdd = new SqlParameterHelperDb();
        List<TransactionOb> transactions = new List<TransactionOb>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        paramAdd.AddSqlParameterFromObject(sql, sortRequest, query);
        
        using var reader = await sql.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            transactions.Add(AddValueFromReader(reader));
        }
        return transactions;
    }
    

    private TransactionOb AddValueFromReader(SqlDataReader reader)
    {
         return new  TransactionOb
        {
            TransactionId = reader.GetString(reader.GetOrdinal("TransactionId")),
            UserId = reader.GetString(reader.GetOrdinal("UserId")),
            Date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Date"))),
            AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Income = reader.GetDecimal(reader.GetOrdinal("Income")),
            Outcome = reader.GetDecimal(reader.GetOrdinal("Outcome")),
            ToAccount = reader.GetString(reader.GetOrdinal("ToAccount")),
            FromAccount = reader.GetString(reader.GetOrdinal("FromAccount")),
            IsFixedPayment = reader.GetBoolean(reader.GetOrdinal("IsFixedPayment")),
            FixedPaymentId = reader.IsDBNull(reader.GetOrdinal("FixedPaymentId"))
                ? null
                : reader.GetString(reader.GetOrdinal("FixedPaymentId"))
        };
    }

    
}