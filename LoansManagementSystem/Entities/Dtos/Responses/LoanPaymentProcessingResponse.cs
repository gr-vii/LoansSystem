namespace LoansManagementSystem.Entities.Dtos.Responses;

public class LoanPaymentProcessingResponse
{
    public int Amount { get; set; }
    public Guid LoanId { get; set; }
    public bool IsProcessed { get; set; }
}