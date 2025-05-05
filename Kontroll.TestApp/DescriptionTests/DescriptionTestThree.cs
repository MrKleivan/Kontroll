using Kontroll.Controller;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.TestApp.DescriptionTests;

public class DescriptionTestThree
{
    public async Task Run(IConfiguration config)
    {
        DescriptionController app = new DescriptionController(config);
        DescriptionOb descriptionOb = new DescriptionOb();
        while (true)
        {
            Console.WriteLine("Skriv inn en eksisterende beskrivelse");
            string? externalDescription = Console.ReadLine();
            Console.WriteLine("Skriv inn en ny beskrivelse");
            string? userDescription = Console.ReadLine();
            
            Console.WriteLine($"Eksisterende beskrivelse: {externalDescription} || Din beskrivelse{userDescription} \n");
            
            Console.WriteLine("Vil du legge til? j/n");
            var answer = Console.ReadLine();

            if (answer?.ToLower() == "j")
            {
                descriptionOb = new DescriptionOb
                {
                    UserId = "1e21c816-5591-40ca-b418-fd4c7c8ef188",
                    ExternalDescription = externalDescription,
                    UserDescription = userDescription,
                };    
                
                await app.AddDescription(descriptionOb);
                break;
            }
            
        }
        

        List<DescriptionOb> des = await app.GetDescriptionByUserId(descriptionOb);

        foreach (var d in des)
        {
            Console.WriteLine(d.UserId + " : " + d.Id +":" + d.ExternalDescription + " : " + d.UserDescription);
        }
    }
}