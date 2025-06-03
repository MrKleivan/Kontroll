using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kontroller.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionApiController : ControllerBase
{
    private TransactionController  _transactionController;

    public TransactionApiController(IConfiguration config)
    {
        _transactionController = new TransactionController(config);
    }


    [HttpPost]
    public async Task<IActionResult> AddTransaction(TransactionPostRequest transaction) => 
        await _transactionController.AddTransaction(transaction);

    
    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetTransactionByTransactionId(string transactionId)
    {
        TransactionOb? transactionOb = await _transactionController.GetSingleTransactionByTransactionId(new { TransactionId = transactionId });

        if (transactionOb == null)
        {
            return NotFound("Fant ikke transaksjonen");
        }
        return Ok(transactionOb);
    }

    [HttpPost("transactionSort")]
    public async Task<IActionResult> GetTransactionsByCriteria(SortRequest sortRequest)
    {
        List<TransactionOb> transactionObs = await _transactionController.GetUserTransactionsByCriteria(sortRequest);

        if (transactionObs == null)
        {
            return NotFound("Fant ikke transaks'er");
        }
        return Ok(transactionObs);
    }
    
    
    
}