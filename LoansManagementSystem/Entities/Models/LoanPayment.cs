namespace LoansManagementSystem.Entities.Models;

public class LoanPayment : Base
{
    public int Amount { get; set; }
    public Guid LoanId { get; set; }
    public virtual LoanApplication? Loan { get; set; }
}