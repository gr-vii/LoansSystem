using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Entities.Models;
using MediatR;

namespace LoansManagementSystem.Api.Commands.LoanPayments;

public class CreateLoanPaymentInfoRequest : IRequest<LoanPayment>
{
    public CreateLoanPaymentRequest LoanPaymentRequest { get; }

    public CreateLoanPaymentInfoRequest(CreateLoanPaymentRequest clientRequest)
    {
        LoanPaymentRequest = clientRequest;
    }
}