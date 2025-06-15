using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;
using Microsoft.AspNetCore.Mvc;

namespace Kontroller.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FixedTransactionApiController : ControllerBase
{
    private FixedTransactionController _fixedTransactionController;

    public FixedTransactionApiController(IConfiguration config)
    {
        _fixedTransactionController = new FixedTransactionController(config);
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> GetAllFixedExpenses(string userId)
    {
        List<FixedTransactionsOb> fixedTransactionsOb = await _fixedTransactionController.GetFixedExpensesByUserId(userId);

        if (fixedTransactionsOb.Count == 0)
        {
            return NotFound("Fant inngen registrerte faste utgifter");
        }
        return Ok(fixedTransactionsOb);
        
    }
    
    
}