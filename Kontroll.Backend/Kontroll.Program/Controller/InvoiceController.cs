using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class InvoiceController : ControllerBase
{
    private readonly InvoiceDb _db;
    private IConfiguration _configuration;

    public InvoiceController(IConfiguration config)
    {
        _configuration = config;
        _db = new InvoiceDb(config);
    }

    public async Task<bool> AddInvoice(InvoiceOb invoice)
    {
        await CheckIfInnTransactionsAndUpdateInvoice(invoice);
        return await _db.AddInvoiceToDatabase(invoice);
    }

    public async Task<bool> UpdateInvoice(InvoiceOb invoice)
    {
        return await _db.UpdateInvoiceInDatabase(invoice);
    }

    private async Task CheckIfInnTransactionsAndUpdateInvoice(InvoiceOb invoice)
    {
        TransactionController transactionController = new TransactionController(_configuration);

        TransactionOb? transactionOb = await transactionController.GetTransactionByInvoiceValues(invoice);

        if (transactionOb != null)
        {
            invoice.TransactionId = transactionOb.TransactionId;
        }
        
    }

    
}