namespace LoansManagementSystem.Entities.Dtos.Responses;

public class ClientResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
}