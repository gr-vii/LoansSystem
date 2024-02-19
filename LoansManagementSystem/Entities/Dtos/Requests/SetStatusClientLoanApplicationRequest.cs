namespace LoansManagementSystem.Entities.Dtos.Requests;

public class SetStatusClientLoanApplicationRequest
{
    public string Id { get; set; } = null!;
    public bool Status { get; set; }
    public string Errors { get; set; }
}