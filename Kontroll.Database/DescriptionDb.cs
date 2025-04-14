using System.Data.SqlClient;
using Kontroll.Database.Model.TransactionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Model;

public class DescriptionDb
{
    private readonly string? _connectionString;

    public DescriptionDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");;
    }

    public async Task<DescriptionOb> GetDescriptionFromDatabase(string description)
    {
        var query = $"SELECT * FROM Description WHERE DescriptionId = {description}";
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();
        using SqlCommand sql = new SqlCommand(query, connection);
        sql.Parameters.Add(new SqlParameter("@Description", description));
        using SqlDataReader reader = await sql.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new DescriptionOb
            {
                UserId = reader.GetString(reader.GetOrdinal("UserId")),
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                StandarDescription = reader.GetString(reader.GetOrdinal("StandarDescription")),
                UsersDescription = reader.GetString(reader.GetOrdinal("UsersDescription")),
            };
        }
        return null;
    }

    public async Task<bool> UpdateDescriptionInDatabase(DescriptionOb descriptionOb)
    {
        var query = "UPDATE Description SET UserDescription = @UserDescription WHERE DescriptionId = @Id";
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();
        
        using SqlCommand sql = new SqlCommand(query, connection);
        sql.Parameters.Add(new SqlParameter("@UserDescription", descriptionOb.UsersDescription));
        sql.Parameters.Add(new SqlParameter("@Id", descriptionOb.Id));

        return await sql.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteDescriptionFromDatabase(int id)
    {
        var query = "DELETE FROM Description WHERE Id = @Id";
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();
        
        using SqlCommand sql = new SqlCommand(query, connection);
        sql.Parameters.Add(new SqlParameter("@Id", id));
        
        return await sql.ExecuteNonQueryAsync() > 0;
    }
}