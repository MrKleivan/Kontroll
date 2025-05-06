namespace Kontroll.Database.Model.UserModels;

public class UserOb
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<UserAccess> AccessGivenTo { get; set; }
    public List<UserAccess> AccessGrantedFrom { get; set; }
}