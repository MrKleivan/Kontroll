using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class FixedExpensesController : ControllerBase
{
   
    private FixedExpenseDb _db;

    public FixedExpensesController(IConfiguration config)
    {
        _db = new FixedExpenseDb(config);
    }
}