using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.LoanApplications;

public class UpdateClientLoanApplicationInfoRequest : IRequest<bool>
{
    public UpdateClientLoanApplicationRequest ClientLoanApplicationRequest { get; }

    public UpdateClientLoanApplicationInfoRequest(UpdateClientLoanApplicationRequest clientRequest)
    {
        ClientLoanApplicationRequest = clientRequest;
    }
}