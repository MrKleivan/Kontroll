namespace Kontroll.Database.Model.UserModels;

public class UserAccess
{
    public int Id { get; set; }
    public string OwnerUserId { get; set; }
    public string MemberUserId { get; set; }
    public string AccessLevel { get; set; }
    public List<string> AccountNumber { get; set; }
}