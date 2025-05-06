using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database;

public class SupplierDb
{
    private readonly string? _connectionString;
    private SqlParameterHelperDb _sqlParameterHelperDb = new SqlParameterHelperDb();
    private SqlReaderHelperDb _sqlReaderHelperDb = new SqlReaderHelperDb();

    public SupplierDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<bool> AddSupplierToDatabase(SupplierOb supplierOb)
    {
        var query = "INSERT INTO SupplierTb (UserId, SupplierId, CompanyName, CompanyAddress, CompanyPhoneNumber, CompanyEmails, TypeOfGoods) " +
                    "VALUES (@UserId, @SupplierId, @CompanyName,  @CompanyAddress, @CompanyPhoneNumber, @CompanyEmails, @TypeOfGoods)";
        
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
}