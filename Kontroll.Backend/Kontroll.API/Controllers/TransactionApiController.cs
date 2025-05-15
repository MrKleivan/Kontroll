using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kontroller.API.Controllers;

public class TransactionApiController : ControllerBase
{
    private TransactionController  _transactionController;
    // GET
    public async Task<IActionResult> AddTransaction(TransactionPostRequest transaction) => 
        await _transactionController.AddTransaction(transaction);
    
    
}