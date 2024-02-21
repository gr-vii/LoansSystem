using LoansManagementSystem.Utilities;

namespace LoansManagementSystem.Entities.Dtos.Requests;

public class SignAdministratorInRequest
{
    [VerifyAndCorrectPhone]
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}