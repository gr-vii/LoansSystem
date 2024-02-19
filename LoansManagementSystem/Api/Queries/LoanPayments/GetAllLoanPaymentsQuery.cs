using LoansManagementSystem.Entities.Dtos.Responses;
using MediatR;

namespace LoansManagementSystem.Api.Queries.LoanPayments;

public class GetAllLoanPaymentsQuery : IRequest<IEnumerable<LoanPaymentResponse>>
{
    public Guid LoanId { get; }

    public GetAllLoanPaymentsQuery(Guid loanId)
    {
        LoanId = loanId;
    }
}