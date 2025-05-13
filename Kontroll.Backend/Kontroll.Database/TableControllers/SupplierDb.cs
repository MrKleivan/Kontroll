using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class SupplierDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb = new();

    public SupplierDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<bool> AddSupplierToDatabase(SupplierOb supplierOb)
    {
        var query = "INSERT INTO SupplierTb (UserId, SupplierId, SupplierName, SupplierAddress, SupplierPhoneNumber, SupplierEmails, TypeOfGood, WebSites) " +
                    "VALUES (@UserId, @SupplierId, @SupplierName,  @SupplierAddress, @SupplierPhoneNumber, @SupplierEmails, @TypeOfGoods, @WebSite)";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, supplierOb) > 0;
    }

    public async Task<bool> IsSupplierExistingInDatabase(SupplierOb supplierOb)
    {
        var query = "SELECT * FROM SupplierTb WHERE  SupplierId = @SupplierId";
        
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, supplierOb) > 0;
    }
    

    public async Task<List<SupplierOb>> GetAllSuppliersByUserIdFromDatabase(Object obj)
    {
        var query = "SELECT * FROM SupplierTb WHERE UserId = @UserId";
        
        return await _sqlReaderHelperDb.ExecuteReaderAndMapAsync<SupplierOb>(_connectionString, query, obj);
    }

    public async Task<SupplierOb?> GetSupplierFromDatabaseBySupplierName(object obj)
    {
        var query = "SELECT * FROM SupplierTb WHERE SupplierName = @SupplierName";
        
        return await _sqlReaderHelperDb.ExecuteReaderSingleAsync<SupplierOb>(_connectionString, query, obj);
    }
}