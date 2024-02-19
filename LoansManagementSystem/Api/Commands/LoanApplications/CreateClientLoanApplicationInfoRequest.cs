using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.LoanApplications;

public class CreateClientLoanApplicationInfoRequest : IRequest<Guid>
{
    public CreateClientLoanApplicationRequest ClientLoanApplicationRequest { get; }

    public CreateClientLoanApplicationInfoRequest(CreateClientLoanApplicationRequest clientRequest)
    {
        ClientLoanApplicationRequest = clientRequest;
    }
}