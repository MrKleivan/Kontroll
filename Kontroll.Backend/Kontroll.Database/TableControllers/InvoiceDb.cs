using Kontroll.Database.Libary;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database.TableControllers;

public class InvoiceDb
{
    private readonly string? _connectionString;
    private SqlReaderHelperDb _sqlReaderHelperDb = new();

    public InvoiceDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }


    public async Task<bool> AddInvoiceToDatabase(InvoiceOb invoice)
    {
        var query =
            @"INSERT INTO InvoiceTb (UserId, InvoiceId, FixedExpenseId, SupplierId, SupplierName, Description, TypeOfInvoice, InvoiceSentOn, InvoiceAmount, InvoiceKidNumber, IsPaid, SettledDate, TransactionId, HasDebtCollection, DebtCollectionId)"
            + "VALUES (@UserId, @InvoiceId, @FixedExpenseId, @SupplierId, @SupplierName, @Description, @TypeOfInvoice, @InvoiceSentOn, @InvoiceAmount, @InvoiceKidNumber, @IsPaid, @SettledDate, @TransactionId, @HasDebtCollection, @DebtCollectionId)";
       
        return await _sqlReaderHelperDb.ExecuteNonQueryAsync(_connectionString, query, invoice) > 0;
    }
}