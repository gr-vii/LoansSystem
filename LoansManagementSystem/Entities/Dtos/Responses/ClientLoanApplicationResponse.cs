namespace LoansManagementSystem.Entities.Dtos.Responses;

public class ClientLoanApplicationResponse
{
    public Guid ClientId { get; set; }
    public Guid LoanId { get; set; }
    public string FullName { get; set; } = null!;
    public string EmploymentType { get; set; } = null!;
    public int MonthIncome { get; set; }
    public int Term { get; set; }
    public int Amount { get; set; }
    public string Purpose { get; set; } = null!;
}