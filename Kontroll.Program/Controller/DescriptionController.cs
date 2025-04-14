using Kontroll.Database;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kontroll.Controller;

public class DescriptionController : ControllerBase
{
   
    // private readonly ILogger<DescriptionTranslateController> _logger;
    private readonly DescriptionDb _descriptionDb;

    public async Task<DescriptionOb> GetDescriptionFromDataBase(string description) =>
        await _descriptionDb.GetDescriptionFromDatabase(description);

    public async Task<bool> UpdateDescriptionInDatabase(DescriptionOb descriptionOb) =>
        await _descriptionDb.UpdateDescriptionInDatabase(descriptionOb);

    public async Task<bool> DeleteDescriptionFromDatabase(int descriptionId) =>
        await _descriptionDb.DeleteDescriptionFromDatabase(descriptionId);
}