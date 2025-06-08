using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kontroller.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountApiController  : ControllerBase
{
    private BankAccountController _bankAccountController;

    public BankAccountApiController(IConfiguration config)
    {
        _bankAccountController = new BankAccountController(config);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersBankAccounts(string UserId)
    {
        List<BankAccountOb> bankAccountObs = await _bankAccountController.GetAllUsersBankAccounts(new { UserId = UserId });

        if (bankAccountObs.Count > 0)
        {
            return Ok(bankAccountObs);
        }
        
        return BadRequest("No bank accounts found");
    }

    [HttpPost]
    public async Task<IActionResult> AddBankAccount(BankAccountOb bankAccountOb)
    {
        bool IsAdded = await _bankAccountController.AddBankAccount(bankAccountOb);

        if (!IsAdded)
        {
            return BadRequest("Bank account not added");
        }
        return Ok("Bank account added");
    }
    
}