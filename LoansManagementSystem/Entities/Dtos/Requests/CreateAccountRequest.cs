using LoansManagementSystem.Utilities;

namespace LoansManagementSystem.Entities.Dtos.Requests;

public class CreateAccountRequest
{
    [VerifyAndCorrectPhone]
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}