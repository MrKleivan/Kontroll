using Kontroll.Database.Model.UserModels;
using Microsoft.Extensions.Configuration;

namespace Kontroll.Database;

public class UserDb
{
    private readonly string? _connectionString;

    public UserDb(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
                            ?? throw new Exception("ConnectionString not found");
    }

    public void SaveUserAccess(UserAccess userAccess)
    {
        var query = "INSERT INTO ";
    }

    public void DeleteUserAccess(UserAccess userAccess)
    {
        
    }
}