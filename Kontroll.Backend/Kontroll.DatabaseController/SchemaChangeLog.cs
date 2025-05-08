namespace Kontroll.DatabaseController;

public class SchemaChangeLog
{
    public string ChangeType { get; set; } = "";
    public string ColumnName { get; set; } = "";
    public string SqlCommand { get; set; } = "";
}