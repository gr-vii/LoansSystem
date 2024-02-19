namespace LoansManagementSystem.Entities.Dtos.Requests;

public class CreateLoanPaymentRequest
{
    public int Amount { get; set; }
    public Guid LoanId { get; set; }
}