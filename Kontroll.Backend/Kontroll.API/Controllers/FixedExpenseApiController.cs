using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Database.TableControllers;
using Microsoft.AspNetCore.Mvc;

namespace Kontroller.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FixedExpenseApiController : ControllerBase
{
    private FixedExpenseController _fixedExpenseController;

    public FixedExpenseApiController(IConfiguration config)
    {
        _fixedExpenseController = new FixedExpenseController(config);
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> GetAllFixedExpenses(string userId)
    {
        List<FixedExpenseOb> fixedExpenses = await _fixedExpenseController.GetFixedExpensesByUserId(userId);

        if (fixedExpenses.Count == 0)
        {
            return NotFound("Fant inngen registrerte faste utgifter");
        }
        return Ok(fixedExpenses);
        
    }
    
    
}