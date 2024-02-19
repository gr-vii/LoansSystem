using LoansManagementSystem.Utilities;

namespace LoansManagementSystem.Entities.Dtos.Requests;

public class UpdateClientRequest
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    [VerifyAndCorrectPhone]
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
}