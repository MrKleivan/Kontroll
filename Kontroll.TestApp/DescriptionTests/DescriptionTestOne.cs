using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Kontroll.Model;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.DescriptionTests;

public class DescriptionTestOne
{
    
    public async Task Run(IConfiguration config)
    {
        DescriptionController app = new DescriptionController(config);
        
        DescriptionOb DescriptionOb = new DescriptionOb
        {
            UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
            ExternalDescription = "KIWI 555 AUSTBY TINN AUSTBYG TINN AUSTBYGD",
            UserDescription = "Matvarer",
        };
        
        await app.AddDescription(DescriptionOb);

        List<DescriptionOb> des = await app.GetDescriptionByUserId(DescriptionOb);

        foreach (var d in des)
        {
            Console.WriteLine(d.UserId + " : " + d.Id +":" + d.ExternalDescription + " : " + d.UserDescription);
        }
    }
}