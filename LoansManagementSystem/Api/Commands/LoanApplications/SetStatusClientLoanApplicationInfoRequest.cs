using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.LoanApplications;

public class SetStatusClientLoanApplicationInfoRequest : IRequest<bool>
{
    public SetStatusClientLoanApplicationRequest ClientLoanApplicationRequest { get; }

    public SetStatusClientLoanApplicationInfoRequest(SetStatusClientLoanApplicationRequest clientRequest)
    {
        ClientLoanApplicationRequest = clientRequest;
    }
}