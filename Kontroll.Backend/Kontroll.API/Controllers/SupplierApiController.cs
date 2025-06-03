using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kontroller.API.Controllers;


[ApiController]
[Route("[controller]")]
public class SupplierApiController : Controller
{
   
    private SupplierController _supplierController;


    public SupplierApiController(IConfiguration config)
    {
        _supplierController = new SupplierController(config);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliersByUserId(string userId)
    {
        List<SupplierOb> supplierObs = await _supplierController.GetAllSuppliersByUserId(new { userId });

        if (supplierObs.Count == 0)
        {
            return NotFound("Fant ikke transaksjonen");
        }
        
        return Ok(supplierObs);
    }

    [HttpPost]
    public async Task<IActionResult> GetUsersBalanceSheetAtSupplier([FromBody] BalanceSheetRequestOb request)
    {
        try
        {
            decimal balance = await _supplierController.GetUsersBalanceSheetAtSupplier(request);
            return Ok(balance);
        }
        catch (Exception ex)
        {
            // Logger evt. feilmelding
            Console.WriteLine(ex.Message);
            return BadRequest("Noe gikk galt. Sjekk input og prøv igjen.");
        }
    } 
    
}