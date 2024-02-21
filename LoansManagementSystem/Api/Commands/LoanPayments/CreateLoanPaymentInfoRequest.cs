using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Entities.Dtos.Responses;
using MediatR;

namespace LoansManagementSystem.Api.Commands.LoanPayments;

public class CreateLoanPaymentInfoRequest : IRequest<LoanPaymentProcessingResponse>
{
    public CreateLoanPaymentRequest LoanPaymentRequest { get; }

    public CreateLoanPaymentInfoRequest(CreateLoanPaymentRequest clientRequest)
    {
        LoanPaymentRequest = clientRequest;
    }
}