using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Kontroll.Database.Libary;

public class SqlParameterHelperDb
{
    public void AddSqlParameterFromObject(SqlCommand sql, object obj, string query)
    {
        if (obj == null || string.IsNullOrWhiteSpace(query)) return;
        
        var property = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var paramNamesInQuery = GetParameterNamesFromQuery(query);

        foreach (var paramName in paramNamesInQuery)
        {
            var prop = property.FirstOrDefault(p => string.Equals(p.Name, paramName, StringComparison.OrdinalIgnoreCase));
            
            if (prop == null) continue;
            
            var value = prop.GetValue(obj);
            var paramFullName = "@" + paramName;
            
            if (sql.Parameters.Contains(paramFullName)) continue;
            
            AddSqlParameterFromString(sql, paramFullName, value);
        }
    }

    public void AddSqlParameterFromString(SqlCommand sql, string paramFullName, object value)
    {
        if (sql.Parameters.Contains(paramFullName)) return;

        if (value is DateOnly dateOnly)
        {
            sql.Parameters.AddWithValue(paramFullName, dateOnly.ToDateTime(TimeOnly.MinValue));
        }
        else if (value == null)
        {
            sql.Parameters.AddWithValue(paramFullName, DBNull.Value);
        }
        else
        {
            sql.Parameters.AddWithValue(paramFullName, value);
        }
    }

    private static List<string> GetParameterNamesFromQuery(string query)
    {
        var parameterNames = Regex.Matches(query, @"@\w+");
        return parameterNames.Cast<Match>()
            .Select(m => m.Value.TrimStart('@'))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}