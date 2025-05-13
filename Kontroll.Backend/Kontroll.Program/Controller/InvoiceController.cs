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
        await CheckIfInnTransactions(invoice);
        return await _db.AddInvoiceToDatabase(invoice);
    }

    private async Task CheckIfInnTransactions(InvoiceOb invoice)
    {
        TransactionController transactionController = new TransactionController(_configuration);

        List<TransactionOb> transactionObs = await transactionController.GetTransactionByInvoiceValues(invoice); 
        
        if (transactionObs.Count > 0)
        {
            await transactionController.AddTransaction()
        }
    }
}