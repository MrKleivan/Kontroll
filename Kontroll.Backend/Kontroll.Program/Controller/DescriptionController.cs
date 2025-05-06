using System.Transactions;
using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class DescriptionController : ControllerBase
{
   
    // private readonly ILogger<DescriptionTranslateController> _logger;
    private readonly DescriptionDb _db;

    public DescriptionController(IConfiguration config)
    {
        _db = new DescriptionDb(config);
    }

    public async Task<DescriptionOb> GetDescriptionByExternalDescription(TransactionPostRequest transaction) =>
        await _db.GetDescriptionFromDatabase(transaction);
    
    public async Task<List<DescriptionOb>> GetDescriptionByUserId([FromBody]DescriptionOb descriptionOb) =>
        await _db.GetAllDescriptionFromDatabaseByUserId(descriptionOb);

    public async Task<bool> AddDescription(DescriptionOb descriptionOb) => 
        await _db.AddDescriptionToDatabase(descriptionOb);

    public async Task<bool> DescriptionExists(TransactionOb transactionOb) =>
        await _db.DescriptionExistsInDatabase(transactionOb) > 0;
    
    public async Task<bool> UpdateDescription(DescriptionOb descriptionOb) =>
        await _db.UpdateDescriptionInDatabase(descriptionOb);

    public async Task<bool> DeleteDescriptionFromDatabase(DescriptionOb descriptionOb) =>
        await _db.DeleteDescriptionFromDatabase(descriptionOb);
}