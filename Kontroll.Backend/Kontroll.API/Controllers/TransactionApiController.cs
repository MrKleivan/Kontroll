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

    [HttpPost("convert-csv")]
    public async Task<IActionResult> ConvertCsvToListOfTransactions(IFormFile file, [FromForm] string userId, [FromForm] string AccountNumber)
    {
        List<TransactionPostRequest> transactionPostRequests =  await _transactionController.ConvertExelToObjectList(file,  userId, AccountNumber);
        
        if (transactionPostRequests != null && transactionPostRequests.Count > 0)
        {
            foreach (var transaction in transactionPostRequests)
            {
                if (transaction.Income > 0)
                {
                    if (transaction.ToAccount != AccountNumber)
                    {
                        return Unauthorized();
                    }
                }
                if (transaction.Outcome < 0)
                {
                    if (transaction.FromAccount != AccountNumber)
                    {
                        return Unauthorized();
                    }
                }
            }
            return Ok(transactionPostRequests);
        }
        return NotFound("Fant ikke transaks'er");
    }
    
    
    
}