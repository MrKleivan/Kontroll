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

    public TransactionDB(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }
    
    
    public async Task<TransactionOb?> GetTransactionFromDatabaseByTransactionId(TransactionOb transaction)
    {
        SortRequest sortRequest = null;
        var query = "SELECT * FROM TransactionTb WHERE TransactionId = @TransactionId";
        
        return await ExecuteReaderAndMapTransaction(transaction, sortRequest, query);
    }

    public async Task<List<TransactionOb>> GetMultipleTransactionsSortedByDate([FromBody] SortRequest sortRequest)
    {
        TransactionOb? transaction = null;
        var query = $"SELECT * FROM TransactionTb WHERE {sortRequest.Request} BETWEEN @StartDate AND @EndDate";
        Console.WriteLine(sortRequest.StartDate);
        return await ExecuteReaderAndMapMultipleTransactions(transaction, sortRequest, query);
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
        TransactionOb? transaction = null;
        
        return await ExecuteReaderAndMapMultipleTransactions(transaction, sortRequest, query);
    }
    
    public async Task<List<TransactionOb>> GetTransactionsSortedByDescription([FromBody] SortRequest sortRequest)
    {
        TransactionOb? transaction = null;
        var query = $"SELECT * FROM TransactionTb WHERE Description = @Description";
        
        return await ExecuteReaderAndMapMultipleTransactions(transaction, sortRequest, query);
    }

    public async Task<bool> AddTransactionToDatabase(TransactionOb transaction)
    {
        var query = "INSERT INTO TransactionTb (TransactionId, UserId, Date, AccountNumber, Description, Income, Outcome, ToAccount, FromAccount, IsFixedPayment, FixedPaymentId)" 
                    + "VALUES (@TransactionId, @UserId, @Date, @AccountNumber, @Description, @Income, @Outcome, @ToAccount, @FromAccount, @IsFixedPayment ,@FixedPaymentId)";
        
        return await ExecuteNonQuery(transaction, query);
    }

    public async Task<bool> DeleteTransactionFromDatabase(string transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateTransactionInDatabase(TransactionOb transaction)
    {
        throw new NotImplementedException();
    }
    
    
    public async Task<bool> TransactionExists(TransactionOb transaction)
    {
        SortRequest sortRequest = null;
        var query = @"
        SELECT COUNT(*) FROM TransactionTb
        WHERE 
            Date = @Date AND 
            AccountNumber = @AccountNumber AND 
            Description = @Description AND 
            Income = @Income AND 
            Outcome = @Outcome";

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var sql = new SqlCommand(query, connection);
        AddSqlParametersFromQuery(sql, transaction, sortRequest);

        var result = (int)await sql.ExecuteScalarAsync();
        return result > 0;
    } 

    private async Task<bool> ExecuteNonQuery(TransactionOb transaction, string query)
    {
        SortRequest sortRequest = null;
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        
        AddSqlParametersFromQuery(sql, transaction, sortRequest);
        
        return await sql.ExecuteNonQueryAsync() > 0;
    }

    private async Task<TransactionOb> ExecuteReaderAndMapTransaction(TransactionOb transaction, SortRequest sortRequest, string query)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        AddSqlParametersFromQuery(sql, transaction,  sortRequest);
        
        using var reader = await sql.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return AddValueFromReader(reader);
        }

        return null;
    }
    
    private async Task<List<TransactionOb>> ExecuteReaderAndMapMultipleTransactions(TransactionOb transaction, SortRequest sortRequest, string query)
    {
        List<TransactionOb> transactions = new List<TransactionOb>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        AddSqlParametersFromQuery(sql, transaction, sortRequest);
        
        using var reader = await sql.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            transactions.Add(AddValueFromReader(reader));
        }
        return transactions;
    }
    
    private void AddSqlParametersFromQuery(SqlCommand sql, TransactionOb transaction, SortRequest sortRequest)
    {
        if (transaction != null)
        {
            sql.Parameters.AddWithValue("@TransactionId", transaction.TransactionId);
            sql.Parameters.AddWithValue("@UserId", transaction.UserId);
            sql.Parameters.AddWithValue("@Date", transaction.Date.ToDateTime(TimeOnly.MinValue));
            sql.Parameters.AddWithValue("@AccountNumber", transaction.AccountNumber);
            sql.Parameters.AddWithValue("@Description", transaction.Description);
            sql.Parameters.AddWithValue("@Income", transaction.Income);
            sql.Parameters.AddWithValue("@Outcome", transaction.Outcome);
            sql.Parameters.AddWithValue("@ToAccount", transaction.ToAccount);
            sql.Parameters.AddWithValue("@FromAccount", transaction.FromAccount);
            sql.Parameters.AddWithValue("@IsFixedPayment", transaction.IsFixedPayment);
            sql.Parameters.AddWithValue("@FixedPaymentId", 
                                        transaction.FixedPaymentId is null
                                        ? DBNull.Value
                                        : transaction.FixedPaymentId);
        }
        if (sortRequest != null)
        {
            AddIfNotNull(sql,"@UserId", sortRequest.UserId);
            AddIfNotNull(sql,"@TransactionId", sortRequest.TransactionId);
            AddIfNotNull(sql,"@Request", sortRequest.Request);
            AddIfNotNull(sql,"@Description", sortRequest.Description);
            AddIfNotNull(sql, "@AccountNumber", sortRequest.AccountNumber);
            AddIfNotNull(sql,"@MinAmount", sortRequest.MinAmount);
            AddIfNotNull(sql,"@MaxAmount", sortRequest.MaxAmount);
            AddIfNotNull(sql,"@AmountDirection", sortRequest.AmountDirection);
            if (sortRequest.StartDate.HasValue)
            {
                AddIfNotNull(sql,"@StartDate", sortRequest.StartDate.Value.ToDateTime(TimeOnly.MinValue));
            }

            if (sortRequest.EndDate.HasValue)
            {
                AddIfNotNull(sql,"@EndDate", sortRequest.EndDate.Value.ToDateTime(TimeOnly.MinValue));
            }
            
        }
        
    }
    
    void AddIfNotNull(SqlCommand sql, string name, object? value)
    {
        if (value != null)
            sql.Parameters.AddWithValue(name, value);
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