namespace LoansManagementSystem.Security;

public interface IJwt
{
    string UserId { get; set; }
    string UserRole { get; set; }
}