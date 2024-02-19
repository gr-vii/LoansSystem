namespace LoansManagementSystem.Entities.Dtos.Responses;

public class LoanPaymentResponse
{
    public DateTime PaymentDate { get; set; }
    public int AmountPaid { get; set; }
    public int TotalAmountPaid { get; set; }
    public int TotalAmountLeft { get; set; }
    public int TermTimeLeft { get; set; }
    public Guid LoanId { get; set; }
}