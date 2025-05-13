using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class DescriptionDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelper = new();
    
    public DescriptionDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<int> DescriptionExistsInDatabase(TransactionOb transactionOb)
    {
        var query = "SELECT COUNT(*) FROM DescriptionTb WHERE ExternalDescription = @ExternalDescription";
        
        int count = await _sqlReaderHelper.ExecuteScalarAsync<int>(_connectionString, query, transactionOb);
        return count;
    }

    public async Task<DescriptionOb> GetDescriptionFromDatabase(TransactionPostRequest transaction)
    {
        var query = "SELECT * FROM DescriptionTb WHERE UserId = @UserId";
        List<DescriptionOb> descriptionObList = await _sqlReaderHelper.ExecuteReaderAndMapAsync<DescriptionOb>(_connectionString, query, transaction);
        DescriptionOb descriptionOb = descriptionObList.Find(d => d.ExternalDescription == transaction.ExternalDescription);
        return descriptionOb;
    }
    
    public async Task<List<DescriptionOb>> GetAllDescriptionFromDatabaseByUserId(DescriptionOb descriptionOb)
    {
        var query = "SELECT * FROM DescriptionTb WHERE UserId = @UserId";
        
        return await _sqlReaderHelper.ExecuteReaderAndMapAsync<DescriptionOb>(_connectionString, query, descriptionOb);
    }

    public async Task<bool> AddDescriptionToDatabase(DescriptionOb descriptionOb)
    {
        var query = "INSERT INTO DescriptionTb (UserId, ExternalDescription, UserDescription) VALUES (@UserId, @ExternalDescription, @UserDescription)";
        
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