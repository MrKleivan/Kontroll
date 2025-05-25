using Kontroll.Database.Model.TransactionModels;
using Kontroll.DatabaseController;

while (true)
{
    Console.WriteLine("Velg vilken tabell du vil oppdatere \n" +
                      "1 TransactionTb \n" +
                      "2 FixedExpenseTb \n" +
                      "3 SupplierTb \n" +
                      "4 DescriptionTb \n" +
                      "5 InvoiceTb \n" +
                      "e For å avslutte");

    string nummer = Console.ReadLine();

    if (nummer == "e")
    {
        break;
    }

    var syncService = new SchemaSyncService("Server=ENTER;Database=Kontroll;Trusted_Connection=True;TrustServerCertificate=True;");
    List<SchemaChangeLog> changes =  new List<SchemaChangeLog>();
    switch (nummer)
    {
        case "1":
            changes = await syncService.SyncClassToTableWithLogAsync<TransactionOb>(dryRun: false);
            break;
        case "2":
            changes = await syncService.SyncClassToTableWithLogAsync<FixedExpenseOb>(dryRun: false);
            break;
        case "3":
            changes = await syncService.SyncClassToTableWithLogAsync<SupplierOb>(dryRun: false);
            break;
        case "4":
            changes = await syncService.SyncClassToTableWithLogAsync<DescriptionOb>(dryRun: false);
            break;
        case "5":
            changes = await syncService.SyncClassToTableWithLogAsync<InvoiceOb>(dryRun: false);
            break;
    }



    foreach (var change in changes)
    {
        Console.WriteLine($"{change.ChangeType}: {change.ColumnName}");
        Console.WriteLine($"SQL: {change.SqlCommand}");
    }
    
}
