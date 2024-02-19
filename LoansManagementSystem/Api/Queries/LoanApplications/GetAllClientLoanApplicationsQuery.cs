using LoansManagementSystem.Entities.Dtos.Responses;
using MediatR;

namespace LoansManagementSystem.Api.Queries.LoanApplications;

public class GetAllClientLoanApplicationsQuery : IRequest<IEnumerable<ClientLoanApplicationResponse>>
{
    public Guid ClientId { get; }

    public GetAllClientLoanApplicationsQuery(Guid clientId)
    {
        ClientId = clientId;
    }
}