using System.Data.SqlClient;
using Kontroll.Database.Model.UserModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database;

public class UserAccessDb
{
    private readonly string? _connectionString;

    public UserAccessDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }

    public async Task<bool> SaveUserAccess(UserAccess userAccess)
    {
        var query = "INSERT INTO UserAccess (Id, OwnerUserId, MemberUserId, AccessLevel, AccountNumber)"
                            +"VALUES (@Id, @OwnerUserId, @MemberUserId, @AccessLevel, @AccountNumber)";
        
        using SqlConnection  connection = new(_connectionString);
        
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        sql.Parameters.AddWithValue("@Id", userAccess.Id);
        sql.Parameters.AddWithValue("@OwnerUserId", userAccess.OwnerUserId);
        sql.Parameters.AddWithValue("@MemberUserId", userAccess.MemberUserId);
        sql.Parameters.AddWithValue("@AccessLevel", userAccess.AccessLevel);
        sql.Parameters.AddWithValue("@AccountNumber", userAccess.AccountNumber);
        
        return await sql.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteUserAccess(UserAccess userAccess)
    {
        var  query = "DELETE FROM UserAccess  WHERE Id = @Id";
        
        using SqlConnection  connection = new(_connectionString);
        
        await connection.OpenAsync();
        
        using var sql = new SqlCommand(query, connection);
        sql.Parameters.AddWithValue("@Id", userAccess.Id);
        
        return await sql.ExecuteNonQueryAsync() > 0;
    }
}