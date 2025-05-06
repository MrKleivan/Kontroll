using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;

namespace Kontroll.Controller;

public class SupplierController
{
    private readonly SupplierDb _db;

    public SupplierController(IConfiguration config)
    {
        _db = new SupplierDb(config);
    }

    public async Task<bool> AddSupplier(SupplierOb supplierOb)
    {
        var IsExistingSupplier = await IsSupplierExisting(supplierOb);
        if (!IsExistingSupplier)
        {
            return await _db.AddSupplierToDatabase(supplierOb);
        }
        return false;
    }

    public async Task<bool> IsSupplierExisting(SupplierOb supplierOb)
    {
        return await _db.IsSupplierExistingInDatabase(supplierOb);
    }
    

    public async Task<List<SupplierOb>> GetAllSuppliersByUserId(Object obj)
    {
        return await _db.GetAllSuppliersByUserIdFromDatabase(obj);
    }
}