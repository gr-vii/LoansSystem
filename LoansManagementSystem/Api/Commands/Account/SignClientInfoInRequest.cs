using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.Account;

public class SignClientInfoInRequest : IRequest<string>
{
    public SignClientInRequest UserRequest { get; }

    public SignClientInfoInRequest(SignClientInRequest userRequest)
    {
        UserRequest = userRequest;
    }
}