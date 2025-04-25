using System.Data.SqlClient;
using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Model;

public class DescriptionDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelper = new SqlReaderHelperDb();
    public DescriptionDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<int> DescriptionExistsInDatabase(TransactionOb transactionOb)
    {
        var query = "SELECT COUNT(*) FROM DescriptionTb WHERE StandarDescription = @Description";
        
        int count = await _sqlReaderHelper.ExecuteScalarAsync<int>(_connectionString, query, transactionOb);
        Console.WriteLine(count);
        return count;
    }

    public async Task<DescriptionOb> GetDescription(TransactionPostRequest transaction)
    {
        var query = "SELECT * FROM DescriptionTb WHERE UserId = @UserId";
        List<DescriptionOb> descriptionObList = await _sqlReaderHelper.ExecuteReaderAndMapAsync<DescriptionOb>(_connectionString, query, transaction);
        DescriptionOb descriptionOb = descriptionObList.Find(d => d.StandarDescription == transaction.Description);
        return descriptionOb;
    }
    public async Task<List<DescriptionOb>> GetAllDescriptionFromDatabaseByUserId(DescriptionOb descriptionOb)
    {
        var query = "SELECT * FROM DescriptionTb WHERE UserId = @UserId";
        
        return await _sqlReaderHelper.ExecuteReaderAndMapAsync<DescriptionOb>(_connectionString, query, descriptionOb);
    }

    public async Task<bool> AddDescriptionToDatabase(DescriptionOb descriptionOb)
    {
        var query = "INSERT INTO DescriptionTb (UserId, StandarDescription, UsersDescription) VALUES (@UserId, @StandarDescription, @UsersDescription)";
        
        return await _sqlReaderHelper.ExecuteNonQueryAsync(_connectionString, query, descriptionOb) > 0;
    }
    
    public async Task<bool> UpdateDescriptionInDatabase(DescriptionOb descriptionOb)
    {
        var query = "UPDATE DescriptionTb SET UserDescription = @UserDescription WHERE DescriptionId = @Id";
        
        return await _sqlReaderHelper.ExecuteNonQueryAsync(_connectionString, query, descriptionOb) > 0;
    }

    public async Task<bool> DeleteDescriptionFromDatabase(DescriptionOb descriptionOb)
    {
        var query = "DELETE FROM DescriptionTb WHERE Id = @Id";
        
        return await _sqlReaderHelper.ExecuteNonQueryAsync(_connectionString, query, descriptionOb) > 0;
        
    }
}