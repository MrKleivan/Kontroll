using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontroll.DatabaseController;

public class SchemaSyncService
{
    private readonly string _connectionString;

    public SchemaSyncService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<SchemaChangeLog>> SyncClassToTableWithLogAsync<T>(bool dryRun = false)
    {
        var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
        var tableNameRaw = tableAttr?.Name ?? typeof(T).Name;
        var tableName = $"dbo.{tableNameRaw}";
        
        var logs = new List<SchemaChangeLog>();
        
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        var existingColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        using (var command = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName AND TABLE_SCHEMA = 'dbo'", connection))
        {
            command.Parameters.AddWithValue("@TableName", tableNameRaw);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                existingColumns.Add(reader.GetString(0));
            }
        }
        
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            if (!existingColumns.Contains(prop.Name))
            {
                string sqlType = GetSqlType(prop.PropertyType);
                string alterSql = $"ALTER TABLE {tableName} ADD {prop.Name} {sqlType}";
                
                logs.Add(new SchemaChangeLog
                {
                    ChangeType = "Add",
                    ColumnName = prop.Name,
                    SqlCommand = alterSql
                });

                if (!dryRun)
                {
                    using var alterCmd = new SqlCommand(alterSql, connection);
                    await alterCmd.ExecuteNonQueryAsync();
                }
            }
            else
            {
                existingColumns.Remove(prop.Name);
            }
        }

        foreach (var obsoleteColumn in existingColumns)
        {
            string dropSql = $"ALTER TABLE {tableName} DROP COLUMN {obsoleteColumn}";

            logs.Add(new SchemaChangeLog
            {
                ChangeType = "Remove",
                ColumnName = obsoleteColumn,
                SqlCommand = dropSql
            });

            if (!dryRun)
            {
                using var dropCmd = new SqlCommand(dropSql, connection);
                await dropCmd.ExecuteNonQueryAsync();
            }
        }
        
        return logs;
    }
    
    private string GetSqlType(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;

        return type.Name switch
        {
            nameof(Int32) => "INT",
            nameof(Int64) => "BIGINT",
            nameof(String) => "NVARCHAR(MAX)",
            nameof(Decimal) => "DECIMAL(18,2)",
            nameof(Boolean) => "BIT",
            nameof(DateTime) => "DATETIME",
            nameof(DateOnly) => "DATE",
            _ => throw new NotSupportedException($"Type {type.Name} is not supported")
        };
    }
}