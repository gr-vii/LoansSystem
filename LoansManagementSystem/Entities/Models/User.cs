namespace LoansManagementSystem.Entities.Models;

public class User : Base
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public int FailedLoginCounter { get; set; }
    public bool Blocked { get; set; }
}