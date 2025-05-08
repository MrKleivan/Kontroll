using Kontroll.Database.Model.TransactionModels;
using Kontroll.DatabaseController;

Console.WriteLine("Velg vilken tabell du vil oppdatere" +
                  "1 TransactionTb" +
                  "2 FixedExpenseTb" +
                  "3 SupplierTb" +
                  "4 DescriptionTb");

string nummer = Console.ReadLine();



var syncService = new SchemaSyncService("Server=KLEIVAN\\SQLEXPRESS;Database=KontrollDb;Trusted_Connection=True;TrustServerCertificate=True;");
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
        changes = await syncService.SyncClassToTableWithLogAsync<DescriptionOb>(dryRun: false);;
        break;
}



foreach (var change in changes)
{
    Console.WriteLine($"{change.ChangeType}: {change.ColumnName}");
    Console.WriteLine($"SQL: {change.SqlCommand}");
}