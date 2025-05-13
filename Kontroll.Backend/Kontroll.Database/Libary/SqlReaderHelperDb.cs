using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Kontroll.Database.Libary;

public class SqlReaderHelperDb
{
    private SqlParameterHelperDb _sqlParamHelperDb = new();
    
    public T MapReaderToObject<T>(SqlDataReader reader) where T : new()
    {
        var obj = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        for (int i = 0; i < reader.FieldCount; i++)
        {
            var columnName = reader.GetName(i);
            object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
            
            var property = properties.FirstOrDefault(p => string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

            if (property == null || !property.CanWrite) continue;

            try
            {
                if (value != null)
                {
                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    
                    if (targetType == typeof(DateOnly) && value is DateTime dt)
                    {
                        property.SetValue(obj, DateOnly.FromDateTime(dt));
                    }
                    else
                    {
                        property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Feil ved mapping av {property.Name}: {ex.Message}");
            }
        }
        return obj;
    }

    public async Task<T> ExecuteScalarAsync<T>(string connectionstring, string query, object? paramSource = null)
    {
        using var connection = new SqlConnection(connectionstring);
        await connection.OpenAsync();
        using var command = new SqlCommand(query, connection);
        
        if (paramSource != null)
            _sqlParamHelperDb.AddSqlParameterFromObject(command, paramSource, query);
        
        var result = await command.ExecuteScalarAsync();
        
        if (result == null || result == DBNull.Value) return default;
        
        return (T)Convert.ChangeType(result, typeof(T));
    }

    public async Task<int> ExecuteNonQueryAsync(string? connectionstring, string query, object? paramSource = null)
    {
        using var connection = new SqlConnection(connectionstring);
        await connection.OpenAsync();
        
        using var command = new SqlCommand(query, connection);
        
        if (paramSource != null)
            _sqlParamHelperDb.AddSqlParameterFromObject(command, paramSource, query);
        
        return await command.ExecuteNonQueryAsync();
    }

    public async Task<T?> ExecuteReaderSingleAsync<T>(string? connectionstring, string query, object? paramSource = null) where T : new()
    {
        using var connection = new SqlConnection(connectionstring);
        await connection.OpenAsync();
        
        using var command = new SqlCommand(query, connection);
        
        if (paramSource != null)
            _sqlParamHelperDb.AddSqlParameterFromObject(command, paramSource, query);
        
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapReaderToObject<T>(reader);
        }
        return default;
    }

    public async Task<List<T>> ExecuteReaderAndMapAsync<T>(string? connectionString, string query, object? paramSource) where T : new()
    {
        var results = new List<T>();

        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        
        using var command = new SqlCommand(query, connection);

        if (paramSource != null)
        {
            _sqlParamHelperDb.AddSqlParameterFromObject(command, paramSource, query);
        }

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var item = MapReaderToObject<T>(reader);
            results.Add(item);
        }
        return results;
    }
}